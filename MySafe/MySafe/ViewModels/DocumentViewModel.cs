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
using MySafe.Mediator.Sheets.GetFile;
using MySafe.Mediator.Sheets.UploadFile;
using MySafe.Models.Responses;
using MySafe.Services.Abstractions;
using NetStandardCommands;
using Prism.Services.Dialogs;
using Prism.Services.Dialogs.Xaml;
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
            var queryResponse = await _mediator.Send(new FileQuery(_jwtToken, attachment.Id));

            if (queryResponse == null) 
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось скачать файл, что-то пошло не так... ", "Ok");

            await File.WriteAllBytesAsync(
                Path.Combine(FileSystem.AppDataDirectory, attachment.Name + attachment.FileExtensions ?? ".jpeg"),
                queryResponse);

            var file = await FileSystem.OpenAppPackageFileAsync(Path.Combine(FileSystem.AppDataDirectory, attachment.Name + attachment.FileExtensions ?? ".jpeg"));
            
        });

        public AsyncCommand UploadFileCommand =>
            _uploadFileCommand ??= new AsyncCommand(async () =>
        {
            var result = await FilePicker.PickAsync(PickOptions.Default);
            var queryResponse = await _mediator.Send(new UploadFileCommand(_jwtToken, Document.Id, result));


            if (queryResponse.IsSuccessful)
            {
                ActionAfterLoadPage();
                return;
            }
            
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
        });
    }
}
