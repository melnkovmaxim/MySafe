using AutoMapper;
using MediatR;
using MySafe.Business.Mediator.Documents.DocumentInfoQuery;
using MySafe.Business.Mediator.Images.OriginalImageQuery;
using MySafe.Business.Mediator.Images.UploadImageCommand;
using MySafe.Business.Mediator.Sheets.OriginalSheetQuery;
using MySafe.Business.Mediator.Sheets.SheetMoveToTrashCommand;
using MySafe.Business.Mediator.Sheets.UploadSheetCommand;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using Prism.Navigation;
using RestSharp;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Images.ImageMoveToTrashCommand;
using MySafe.Data.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Image = MySafe.Core.Entities.Responses.Image;

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
        private AsyncCommand<Attachment> _openFileCommand;

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
            var queryResponse = await _mediator.Send(new DocumentInfoQuery(_itemId.Value));

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

            ResponseBase response;
            if (attachment.IsImage)
            {
                response = await _mediator.Send(new OriginalImageQuery(attachment.Id));
            }
            else
            {
                response = await _mediator.Send(new OriginalSheetQuery(attachment.Id));
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
        });

        public AsyncCommand<Attachment> OpenFileCommand =>
            _openFileCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
            {
                var downloadPath = Ioc.Resolve<IStoragePathesRepository>().DownloadPath;
                var filename = attachment.Name + (attachment.FileExtension ?? ".jpeg");
                var path = Path.Combine(downloadPath, filename);

                if (!File.Exists(path))
                    await _downloadFileCommand.ExecuteAsync(attachment);

                await Launcher.OpenAsync
                (new OpenFileRequest()
                {
                    File = new ReadOnlyFile(path)
                }
                );
            });

        public AsyncCommand UploadFileCommand =>
            _uploadFileCommand ??= new AsyncCommand(async () =>
        {
            var result = await FilePicker.PickAsync(PickOptions.Default);

            await using var stream = await result.OpenReadAsync();
            await using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();

            IResponse response;

            if (result.ContentType.Split('/')[0] == "image")
            {
                response = await _mediator.Send(new UploadImageCommand(Document.Id, result.FileName, result.ContentType, bytes));
            }
            else
            {
                response = await _mediator.Send(new UploadSheetCommand(Document.Id, result.FileName, result.ContentType, bytes));
            }


            if (!response.HasError)
            {
                await ActionAfterLoadPage();
                return;
            }

            await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
        });

        public AsyncCommand<Attachment> MoveToTrashCommand =>
            _moveToTrashCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
        {
            ResponseBase response;

            if (attachment.IsImage)
            {
                response = await _mediator.Send(new ImageMoveToTrashCommand(attachment.Id));
            }
            else
            {
                response = await _mediator.Send(new SheetMoveToTrashCommand(attachment.Id));
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
