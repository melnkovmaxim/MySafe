using System;
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
        private readonly IPrintService _printService;
        private AsyncCommand<Attachment> _downloadFileCommand;
        private AsyncCommand _uploadFileCommand;
        private AsyncCommand<Attachment> _moveToTrashCommand;
        private AsyncCommand<Attachment> _openFileCommand;
        private AsyncCommand<Attachment> _printCommand;

        public bool IsVisibleRefreshFrame => RefreshCommand.IsExecuting || DownloadFileCommand.IsExecuting ||
                                             UploadFileCommand.IsExecuting || MoveToTrashCommand.IsExecuting ||
                                             OpenFileCommand.IsExecuting || PrintCommand.IsExecuting;
        public Document Document { get; set; }
        public ObservableCollection<Attachment> Attachments { get; set; }
        public Attachment CurrentAttachment { get; set; }

        public DocumentViewModel(
            INavigationService navigationService, 
            IMediator mediator, 
            IMapper mapper, 
            IPermissionService permissionService, 
            IFileService fileService,
            IPrintService printService)
            : base(navigationService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _permissionService = permissionService;
            _fileService = fileService;
            _printService = printService;
            Attachments = new ObservableCollection<Attachment>();
        }

        public AsyncCommand<Attachment> DownloadFileCommand =>
            _downloadFileCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
        {
            var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();
            
            if (!isPermissionGranted) return;

            var isSuccessful = await _fileService.TryDownloadAndSaveFile(attachment.Id, attachment.Type, attachment.Name, attachment.FileExtension);
            
            if (!isSuccessful)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось скачать файл, что-то пошло не так... ", "Ok");
            }
        });

        public AsyncCommand<Attachment> OpenFileCommand =>
            _openFileCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
        {
            var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();
            
            if (!isPermissionGranted) return;

            var isSuccessful = await _fileService.TryDownloadAndSaveIfNotExist(attachment.Id, attachment.Type, attachment.Name, attachment.FileExtension);

            if (!isSuccessful)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось скачать файл, что-то пошло не так... ", "Ok");
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
            {
                response = await _mediator.Send(new UploadImageCommand(Document.Id, result.FileName, result.ContentType, bytes));
            }
            else
            {
                response = await _mediator.Send(new UploadSheetCommand(Document.Id, result.FileName, result.ContentType, bytes));
            }

            if (!response.HasError)
            {
                var mediatorResult = await _refreshTask;
                RefillObservableCollection(mediatorResult);
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

        public AsyncCommand<Attachment> PrintCommand => _printCommand ??= new AsyncCommand<Attachment>(async (attachment) =>
        {
            var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();
            
            if (!isPermissionGranted) return;

            var path = _fileService.GetFullPathFileOnDevice(attachment.Name, attachment.FileExtension);

            if (!File.Exists(path))
            {
                await _downloadFileCommand.ExecuteAsync(attachment);
            }

            if (attachment.IsImage)
            {
                var bytes = await File.ReadAllBytesAsync(path);
                
                await Plugin.Printing.CrossPrinting.Current.PrintImageFromByteArrayAsync(
                    bytes, 
                    new Plugin.Printing.PrintJobConfiguration($"Printing {attachment.Name + attachment.FileExtension}"));
                
                return;
            }
            
            if (attachment.FileExtension == ".pdf")
            {
                await using var stream = File.OpenRead(path);
                await Plugin.Printing.CrossPrinting.Current.PrintPdfFromStreamAsync(stream,
                    new Plugin.Printing.PrintJobConfiguration($"Printing {attachment.Name + attachment.FileExtension}"));

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "К печати допускаются только изображения и файлы формата pdf", "Ok");
            }
        });

        protected override Task<Core.Entities.Responses.Document> _refreshTask => _mediator.Send(new DocumentInfoQuery(_itemId.Value), GetCancellationToken());

        protected override void RefillObservableCollection(Core.Entities.Responses.Document mediatorResponse)
        {
            Document = _mapper.Map<Document>(mediatorResponse);
            Attachments.Clear();
            Document.Attachments.ForEach(Attachments.Add);
        }
    }
}
