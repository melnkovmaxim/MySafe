﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Interfaces.Services;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.EntityExtensions;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Services.Mediator.Documents.ChangeDocumentCommand;
using MySafe.Services.Mediator.Documents.DocumentInfoQuery;
using MySafe.Services.Mediator.Images.ChangeImageCommand;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.Presentation.ViewModels
{
    public class DocumentViewModel : AuthorizedRefreshViewModel<DocumentEntity, Document>
    {
        private readonly IFileService _fileService;
        private readonly IFileRestService _fileRestService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IPermissionService _permissionService;

        public AsyncCommand<Attachment> DownloadFileCommand { get; }
        public AsyncCommand<Attachment> MoveToTrashCommand { get; }
        public AsyncCommand<Attachment> OpenFileCommand { get; }
        public AsyncCommand<Attachment> PrintCommand { get; }
        public AsyncCommand UploadFileCommand { get; }
        public AsyncCommand RotatePlusCommand { get; }
        public AsyncCommand RotateMinusCommand { get; }
        public AsyncCommand EditDocumentCommand { get; }

        public Document Document { get; set; }
        public ObservableCollection<Attachment> Attachments { get; set; }
        public Attachment CurrentAttachment { get; set; }

        public DocumentViewModel(
            INavigationService navigationService,
            IMediator mediator,
            IMapper mapper,
            IPermissionService permissionService,
            IFileService fileService,
            IAuthService authService,
            IFileRestService fileRestService)
            : base(navigationService, mapper, authService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _permissionService = permissionService;
            _fileService = fileService;
            _fileRestService = fileRestService;
            Attachments = new ObservableCollection<Attachment>();

            MoveToTrashCommand = new AsyncCommand<Attachment>(MoveToTrashCommandTask);
            OpenFileCommand = new AsyncCommand<Attachment>(OpenFileCommandTask);
            UploadFileCommand = new AsyncCommand(UploadFileCommandTask);
            DownloadFileCommand = new AsyncCommand<Attachment>(DownloadFileCommandTask);
            PrintCommand = new AsyncCommand<Attachment>(PrintCommandTask);
            EditDocumentCommand = new AsyncCommand(EditDocumentCommandTask);

            RotatePlusCommand =
                new AsyncCommand(() => _mediator.Send(new ChangeImageCommand(CurrentAttachment.Id, "+")));
            RotateMinusCommand =
                new AsyncCommand(() => _mediator.Send(new ChangeImageCommand(CurrentAttachment.Id, "-")));
        }


        public async Task DownloadFileCommandTask(Attachment attachment)
        {
            var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();

            if (!isPermissionGranted) return;

            var isSuccessful = await _fileService.TryDownloadAndSaveFile(attachment.Id, attachment.Type, attachment.Name, attachment.FileExtension);

            if (!isSuccessful)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось скачать файл, что-то пошло не так... ", "Ok");
            }
        }

        public async Task OpenFileCommandTask(Attachment attachment)
        {
            var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();
            if (!isPermissionGranted) return;

            var isOpened = await _fileService.TryOpenFileAsync(attachment.Id, attachment.Type, attachment.Name, attachment.FileExtension);

            if (!isOpened)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Ошибка при открытии файла", "Ok");
            }
        }

        public async Task UploadFileCommandTask()
        {
            var pickedFile = await _fileService.GetPickedFileResult();
            var uploadResult = await _fileRestService.UploadAsync(Document.Id, pickedFile);

            if (uploadResult.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                return;
            }

            var mediatorResult = (await _refreshTask).ToDocumentToPresentationModel();
            RefillObservableCollection(mediatorResult);
        }

        public async Task MoveToTrashCommandTask(Attachment attachment)
        {
            var result = await _fileRestService.MoveToTrashAsync(attachment.Id, attachment.Type);

            if (result.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка","Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                return;
            }

            Attachments.Remove(attachment);
        }

        public async Task PrintCommandTask(Attachment attachment)
        {
            var isPermissionGranted = await _permissionService.TryGetStorageWritePermissionAsync();

            if (!isPermissionGranted) return;

            var isPrinted = await _fileService.TryPrintFileAsync(attachment.Id, attachment.Type, attachment.Name, attachment.FileExtension);

            if (!isPrinted)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка","Ошибка при печати файла", "Ok");
            }
        }

        private async Task EditDocumentCommandTask()
        {
            var changedDocumentName = await Application.Current.MainPage.DisplayPromptAsync("Изменить название", Document.Name, accept: "Сохранить", cancel: "Отмена", placeholder: "Новое название");

            if (!string.IsNullOrEmpty(changedDocumentName))
            {
                _ = await _mediator.Send(new ChangeDocumentCommand(changedDocumentName, Document.Location, Document.Id, Document.FolderId));
                await RefreshCommand.ExecuteAsync(null);
            }
        } 

        protected override Task<DocumentEntity> _refreshTask => _mediator.Send(new DocumentInfoQuery(_navigationParameter.ChildId), GetCancellationToken());

        protected override void RefillObservableCollection(Document mediatorEntity)
        {
            Document = _mapper.Map<Document>(mediatorEntity);
            Attachments.Clear();
            Document.Attachments.ForEach(Attachments.Add);
        }
    }
}