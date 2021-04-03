using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Domain.Services;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Services.Mediator.Documents.DocumentInfoQuery;
using MySafe.Services.Mediator.Images.ChangeImageCommand;
using MySafe.Services.Mediator.Images.ImageMoveToTrashCommand;
using MySafe.Services.Mediator.Images.UploadImageCommand;
using MySafe.Services.Mediator.Sheets.SheetMoveToTrashCommand;
using MySafe.Services.Mediator.Sheets.SheetPdfFormatQuery;
using MySafe.Services.Mediator.Sheets.UploadSheetCommand;
using Plugin.Printing;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Document = MySafe.Core.Entities.Responses.Document;

namespace MySafe.Presentation.ViewModels
{
    public class DocumentViewModel : AuthorizedRefreshViewModel<Document>
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IPermissionService _permissionService;
        private AsyncCommand<Attachment> _downloadFileCommand;
        private AsyncCommand<Attachment> _moveToTrashCommand;
        private AsyncCommand<Attachment> _openFileCommand;
        private AsyncCommand<Attachment> _printCommand;
        private AsyncCommand _uploadFileCommand;

        public DocumentViewModel(
            INavigationService navigationService,
            IMediator mediator,
            IMapper mapper,
            IPermissionService permissionService,
            IFileService fileService)
            : base(navigationService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _permissionService = permissionService;
            _fileService = fileService;
            Attachments = new ObservableCollection<Attachment>();

            RotatePlusCommand =
                new AsyncCommand(() => _mediator.Send(new ChangeImageCommand(CurrentAttachment.Id, "+")));
            RotateMinusCommand =
                new AsyncCommand(() => _mediator.Send(new ChangeImageCommand(CurrentAttachment.Id, "-")));
        }

        public static int ID { get; set; }

        public bool IsVisibleRefreshFrame => RefreshCommand.IsExecuting || DownloadFileCommand.IsExecuting ||
                                             UploadFileCommand.IsExecuting || MoveToTrashCommand.IsExecuting ||
                                             OpenFileCommand.IsExecuting || PrintCommand.IsExecuting ||
                                             RotatePlusCommand.IsExecuting;

        public Models.Document Document { get; set; }
        public ObservableCollection<Attachment> Attachments { get; set; }
        public Attachment CurrentAttachment { get; set; }

        public AsyncCommand RotatePlusCommand { get; }
        public AsyncCommand RotateMinusCommand { get; }

        public AsyncCommand<Attachment> DownloadFileCommand =>
            _downloadFileCommand ??= new AsyncCommand<Attachment>(async attachment =>
            {
                var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();

                if (!isPermissionGranted) return;

                var isSuccessful = await _fileService.TryDownloadAndSaveFile(attachment.Id, attachment.Type,
                    attachment.Name, attachment.FileExtension);

                if (!isSuccessful)
                    await Application.Current.MainPage.DisplayAlert("Ошибка",
                        "Не получилось скачать файл, что-то пошло не так... ", "Ok");
            });

        public AsyncCommand<Attachment> OpenFileCommand =>
            _openFileCommand ??= new AsyncCommand<Attachment>(async attachment =>
            {
                var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();

                if (!isPermissionGranted) return;

                var isSuccessful = await _fileService.TryDownloadAndSaveIfNotExist(attachment.Id, attachment.Type,
                    attachment.Name, attachment.FileExtension);

                if (!isSuccessful)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка",
                        "Не получилось скачать файл, что-то пошло не так... ", "Ok");
                    return;
                }

                var path = _fileService.GetFullPathFileOnDevice(attachment.Name, attachment.FileExtension);
                var readOnlyFile = new ReadOnlyFile(path);
                var openFileRequest = new OpenFileRequest(attachment.Name, readOnlyFile);

                await Launcher.OpenAsync(openFileRequest);
            });

        public AsyncCommand UploadFileCommand =>
            _uploadFileCommand ??= new AsyncCommand(async () =>
            {
                var result = await FilePicker.PickAsync(PickOptions.Default);

                await using var stream = await result.OpenReadAsync();
                await using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                var bytes = memoryStream.GetBuffer();

                IResponse response;

                if (result.ContentType.Split('/')[0] == "image")
                    response = await _mediator.Send(new UploadImageCommand(Document.Id, result.FileName,
                        result.ContentType, bytes));
                else
                    response = await _mediator.Send(new UploadSheetCommand(Document.Id, result.FileName,
                        result.ContentType, bytes));

                if (!response.HasError)
                {
                    var mediatorResult = await _refreshTask;
                    RefillObservableCollection(mediatorResult);
                    return;
                }

                await Application.Current.MainPage.DisplayAlert("Ошибка",
                    "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
            });

        public AsyncCommand<Attachment> MoveToTrashCommand =>
            _moveToTrashCommand ??= new AsyncCommand<Attachment>(async attachment =>
            {
                IResponse response;

                if (attachment.IsImage)
                    response = await _mediator.Send(new ImageMoveToTrashCommand(attachment.Id));
                else
                    response = await _mediator.Send(new SheetMoveToTrashCommand(attachment.Id));

                if (response.HasError)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка",
                        "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                    return;
                }

                Attachments.Remove(attachment);
            });

        public AsyncCommand<Attachment> PrintCommand => _printCommand ??= new AsyncCommand<Attachment>(
            async attachment =>
            {
                var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();

                if (!isPermissionGranted) return;

                var path = _fileService.GetFullPathFileOnDevice(attachment.Name, attachment.FileExtension);

                if (!File.Exists(path)) await _downloadFileCommand.ExecuteAsync(attachment);

                if (attachment.IsImage)
                {
                    var bytes = await File.ReadAllBytesAsync(path);

                    await CrossPrinting.Current.PrintImageFromByteArrayAsync(bytes,
                        new PrintJobConfiguration($"Printing {attachment.Name + attachment.FileExtension}"));

                    return;
                }

                Stream stream;

                if (attachment.FileExtension == ".pdf")
                {
                    stream = File.OpenRead(path);
                }
                else
                {
                    var result = await _mediator.Send(new SheetPdfFormatQuery(attachment.Id));
                    stream = new MemoryStream(result.FileBytes);
                }

                _ = await CrossPrinting.Current.PrintPdfFromStreamAsync(stream,
                    new PrintJobConfiguration($"Printing {attachment.Name + attachment.FileExtension}"));

                await stream.DisposeAsync();
            });

        protected override Task<Document> _refreshTask =>
            _mediator.Send(new DocumentInfoQuery(_itemId.Value), GetCancellationToken());

        protected override void RefillObservableCollection(Document mediatorResponse)
        {
            Document = _mapper.Map<Models.Document>(mediatorResponse);
            Attachments.Clear();
            Document.Attachments.ForEach(Attachments.Add);
        }
    }
}