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
using MySafe.Business.Services.Abstractions;
using MySafe.Core.Enums;
using MySafe.Data.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Image = MySafe.Core.Entities.Responses.Image;

namespace MySafe.Presentation.ViewModels
{
    public class DocumentViewModel : AuthorizedRefreshViewModel<Core.Entities.Responses.Document>
    {
        public static int ID { get; set; }
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;
        private readonly IFileService _fileService;
        private AsyncCommand<Attachment> _downloadFileCommand;
        private AsyncCommand _uploadFileCommand;
        private AsyncCommand<Attachment> _moveToTrashCommand;
        private AsyncCommand<Attachment> _openFileCommand;

        public Document Document { get; set; }
        public ObservableCollection<Attachment> Attachments { get; set; }
        public Attachment CurrentAttachment { get; set; }

        public DocumentViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper, IPermissionService permissionService, IFileService fileService)
            : base(navigationService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _permissionService = permissionService;
            _fileService = fileService;
            Attachments = new ObservableCollection<Attachment>();
        }

        public AsyncCommand<Attachment> DownloadFileCommand =>
            _downloadFileCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
        {
            var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();

            if (!isPermissionGranted) return;

            var attachmentType = attachment.IsImage ? AttachmentTypeEnum.Image : AttachmentTypeEnum.Other;
            var isSuccessful = await _fileService.TryDownloadAndSaveFile(attachment.Id, attachmentType, attachment.Name, attachment.FileExtension);

            await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось скачать файл, что-то пошло не так... ", "Ok");
        });

        public AsyncCommand<Attachment> OpenFileCommand =>
            _openFileCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
            {
                var result = await _fileService.DownloadFileAsync(attachment.Id, AttachmentTypeEnum.Image);

                await Launcher.OpenAsync
                (new OpenFileRequest()
                {
                    File = new ReadOnlyFile(_fileService.GetFullPathFileOnDevice(attachment.Name, attachment.FileExtension))
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
                await _refreshTask;
                return;
            }

            await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
        });

        public AsyncCommand<Attachment> MoveToTrashCommand =>
            _moveToTrashCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
        {
            IResponse response;

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

        protected override Task<Core.Entities.Responses.Document> _refreshTask => _mediator.Send(new DocumentInfoQuery(_itemId.Value));

        protected override void RefillObservableCollection(Core.Entities.Responses.Document mediatorResponse)
        {
            Document = _mapper.Map<Document>(mediatorResponse);
            Attachments.Clear();
            Document.Attachments.ForEach(Attachments.Add);
        }
    }
}
