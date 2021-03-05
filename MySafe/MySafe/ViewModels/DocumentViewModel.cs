using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Business.Mediator.Documents.GetDocumentInfo;
using MySafe.Business.Mediator.Images.GetOriginalImage;
using MySafe.Business.Mediator.Images.MoveToTrash;
using MySafe.Business.Mediator.Images.UploadImage;
using MySafe.Business.Mediator.Sheets.GetFile;
using MySafe.Business.Mediator.Sheets.MoveToTrash;
using MySafe.Business.Mediator.Sheets.UploadFile;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Data.Abstractions;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using Prism.Navigation;
using RestSharp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MySafe.Presentation.ViewModels
{
    public class DocumentViewModel : AuthorizedViewModelBase
    {
        public static int ID { get; set; }
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private AsyncCommand<Attachment> _downloadFileCommand;
        private AsyncCommand _uploadFileCommand;
        private AsyncCommand<Attachment> _moveToTrashCommand;

        public Document Document { get; set; }
        public ObservableCollection<Attachment> Attachments { get; set; }
        public Attachment CurrentAttachment { get; set; }

        public DocumentViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper)
            : base(navigationService)
        {
            _mediator = mediator;
            _mapper = mapper;
            Attachments = new ObservableCollection<Attachment>();
        }

        protected override async Task ActionAfterLoadPage()
        {
            var queryResponse = await _mediator.Send(new DocumentInfoQuery(_jwtToken, _itemId.Value));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }

            Document = _mapper.Map<Document>(queryResponse);
            Attachments.Clear();
            Document.Attachments.ForEach(Attachments.Add);
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

            await using var stream = await result.OpenReadAsync();
            await using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            var bytes= memoryStream.ToArray();

            IRestResponse response;

            if (result.ContentType.Split('\\')[0] == "image")
            {
                response = await _mediator.Send(new UploadImageCommand(_jwtToken, Document.Id, result.FileName, result.ContentType, bytes));
            }
            else
            {
                response = await _mediator.Send(new UploadFileCommand(_jwtToken, Document.Id, result.FileName, result.ContentType, bytes));
            }


            if (response.IsSuccessful)
            {
                ActionAfterLoadPage();
                return;
            }
            
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
        });

        public AsyncCommand<Attachment> MoveToTrashCommand => 
            _moveToTrashCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
        {
            BaseResponse response;

            if (attachment.IsImage)
            {
                response = await _mediator.Send(new ImageToTrashCommand(_jwtToken, attachment.Id));
            }
            else
            {
                response = await _mediator.Send(new MoveFileToTrashCommand(_jwtToken, attachment.Id));
            }

            if (response.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                return;
            }

            Attachments.Remove(attachment);
        });
    }
}
