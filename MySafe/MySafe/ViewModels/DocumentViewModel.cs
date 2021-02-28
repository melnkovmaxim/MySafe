using System;
using MediatR;
using MySafe.Models;
using MySafe.ViewModels.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySafe.Core;
using MySafe.Mediator.Documents.GetDocumentInfo;
using MySafe.Mediator.Folders.GetFolderInfo;
using MySafe.Mediator.Images.GetOriginalImage;
using MySafe.Mediator.Images.UploadImage;
using MySafe.Mediator.Sheets.GetFile;
using MySafe.Mediator.Sheets.UploadFile;
using MySafe.Models.Responses;
using MySafe.Repositories.Abstractions;
using MySafe.Services.Abstractions;
using NetStandardCommands;
using Prism.Services.Dialogs;
using Prism.Services.Dialogs.Xaml;
using RestSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace MySafe.ViewModels
{
    public class DocumentViewModel : AuthorizedViewModelBase
    {
        public static int ID { get; set; }
        private readonly IMediator _mediator;
        private AsyncCommand<Attachment> _downloadFileCommand;
        private AsyncCommand _uploadFileCommand;
        private AsyncCommand<Attachment> _moveToTrashCommand;

        public DocumentResponse Document { get; set; }
        public ObservableCollection<Attachment> Attachments { get; set; }
        public Attachment CurrentAttachment { get; set; }

        public DocumentViewModel(INavigationService navigationService, IMediator mediator)
            : base(navigationService)
        {
            _mediator = mediator;
            Attachments = new ObservableCollection<Attachment>();
        }

        protected override async void ActionAfterLoadPage()
        {
            var queryResponse = await _mediator.Send(new DocumentInfoQuery(_jwtToken, _itemId.Value));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }

            Document = queryResponse;
            Attachments.Clear();
            queryResponse.Attachments.ForEach(Attachments.Add);
        }

        public AsyncCommand<Attachment> DownloadFileCommand =>
            _downloadFileCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted) return;
            }

            BaseResponse response;
            if (attachment.IsImage)
            {
                response = await _mediator.Send(new OriginalImageQuery(_jwtToken, attachment.Id));
            }
            else
            {  
                response = await _mediator.Send(new FileQuery(_jwtToken, attachment.Id));
            }

            if (response?.HasError != false)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось скачать файл, что-то пошло не так... ", "Ok");
                return;
            }

            var downloadPath = Ioc.Resolve<IStoragePathesRepository>().DownloadPath;
            var filename = attachment.Name + (attachment.FileExtension ?? ".jpeg");
            await File.WriteAllBytesAsync(
                Path.Combine(downloadPath, filename),
                response.FileBytes);

            var file = await FileSystem.OpenAppPackageFileAsync(Path.Combine(downloadPath, filename));
            
        });

        public AsyncCommand UploadFileCommand =>
            _uploadFileCommand ??= new AsyncCommand(async () =>
        {
            var result = await FilePicker.PickAsync(PickOptions.Default);
            IRestResponse response;

            if (result.ContentType.Split('\\')[0] == "image")
            {
                response = await _mediator.Send(new UploadImageCommand(_jwtToken, Document.Id, result));
            }
            else
            {
                response = await _mediator.Send(new UploadFileCommand(_jwtToken, Document.Id, result));
            }


            if (response.IsSuccessful)
            {
                ActionAfterLoadPage();
                return;
            }
            
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
        });

        public AsyncCommand<Attachment> MoveToTrashCommand => 
            _moveToTrashCommand ??= new AsyncCommand<Attachment>(async (attachment) => {  });
    }
}
