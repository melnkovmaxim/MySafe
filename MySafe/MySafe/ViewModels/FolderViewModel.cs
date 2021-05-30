using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Interfaces.Services;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.EntityExtensions;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Documents.ChangeDocumentCommand;
using MySafe.Services.Mediator.Documents.CreateDocumentCommand;
using MySafe.Services.Mediator.Documents.DocumentInfoQuery;
using MySafe.Services.Mediator.Folders.FolderInfoQuery;
using MySafe.Services.Mediator.Safe.SafeInfoQuery;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MySafe.Presentation.ViewModels
{
    public class FolderViewModel : AuthorizedRefreshViewModel<FolderEntity, Folder>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly Dictionary<string, string> _parentsIconsDictionary = new()
        {
            {"Документы", "docs.png"},
            {"Квартира, Машина, Дача", "auto.png"},
            {"Налоги, Аренда, Платежи", "tax.png"},
            {"Здоровье", "health.png"},
            {"ЖКХ", "util.png"}
        };

        public AsyncCommand<Document> MoveToDocumentCommand { get; }
        public AsyncCommand<Document> EditDocumentNameCommand { get; } 
        public AsyncCommand AddDocumentCommand { get; }
        public AsyncCommand ToggleEditableModeCommand { get; }

        // при установке фильтра, кол-во документов изменяется
        // поэтому храним их дополнительнео в FullDocuments
        public ObservableCollection<Document> Documents { get; set; }
        public ToggleState EditableMode { get; set; }

        private List<Document> FullDocuments { get; set; }
        
        public string FolderName { get; set; }
        public string IconPath { get; set; }
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                FilterDocuments(value);
            }
        }
        
        private int _folderId { get; set; }
        private string _filter;

        public FolderViewModel(INavigationService navigationService, IMapper mapper, IMediator mediator, IAuthService authService)
            : base(navigationService, mapper, authService)
        {
            _mediator = mediator;
            _mapper = mapper;

            Documents = new ObservableCollection<Document>();
            EditableMode = new ToggleState();

            EditDocumentNameCommand = new AsyncCommand<Document>(EditDocumentCommandTask);
            ToggleEditableModeCommand = new AsyncCommand(ToggleEditableModeCommandTask);
            MoveToDocumentCommand = new AsyncCommand<Document>(MoveToDocumentCommandTask);
            AddDocumentCommand = new AsyncCommand(AddDocumentCommandTask);
        }

        private Task ToggleEditableModeCommandTask()
        {
            EditableMode.Toggle();
            return Task.CompletedTask;
        }

        private async Task EditDocumentCommandTask(Document document)
        {
            var changedDocumentName = await Application.Current.MainPage.DisplayPromptAsync("Изменить название", document.Name, accept: "Сохранить", cancel: "Отмена", placeholder: "Новое название");

            if (!string.IsNullOrEmpty(changedDocumentName))
            {
                _ = await _mediator.Send(new ChangeDocumentCommand(changedDocumentName, document.Location, document.Id, document.FolderId));
                await RefreshCommand.ExecuteAsync(null);
            }
        } 

        public async Task MoveToDocumentCommandTask(Document document)
        {
            var @params = new NavigationParameters() { { nameof(NavigationParameter), new NavigationParameter(document.Id, document.Name) }};
            await _navigationService.NavigateAsync(nameof(DocumentPage), @params);
        }

        public async Task AddDocumentCommandTask()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Создать новый документ?", null, "Да", "Нет");
            
            if (!answer) return;

            var response = (await _mediator.Send(new CreateDocumentCommand(_folderId))).ToDocumentToPresentationModel();

            if (response.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось создать документ, что-то пошло не так... ", "Ok");
            }

            var mediatorResult = (await _refreshTask).ToFolderPresentationModel();
            RefillObservableCollection(mediatorResult);
        }

        protected override Task<FolderEntity> _refreshTask => _mediator.Send(new FolderInfoQuery(_navigationParameter.ChildId));

        protected override async void RefillObservableCollection(Folder mediatorEntity)
        {
            var isSuccess = _parentsIconsDictionary.TryGetValue(_navigationParameter.ChildName, out var iconPath);
            IconPath = isSuccess ? iconPath : "other.png";

            FullDocuments = mediatorEntity.Documents;
            Documents.Clear();
            mediatorEntity.Documents.ForEach(Documents.Add);


            var safeFolders = await _mediator.Send(new SafeInfoQuery());
            var currentFolder = safeFolders?.Folders.FirstOrDefault(x => x.Id == mediatorEntity.Id);

            FolderName = currentFolder?.Name
                .Split(":")
                .First()
                .Split(",")
                .FirstOrDefault();
            _folderId = currentFolder?.Id ?? int.MinValue;

            await LoadAttachmentToDocumentPreview();
        }

        private async Task LoadAttachmentToDocumentPreview()
        {
            for (var i = 0; i < Documents.Count; i++)
            {
                var documentEntity = await _mediator.Send(new DocumentInfoQuery(Documents[i].Id), GetCancellationToken());
                var document = _mapper.Map<Document>(documentEntity);
                var attachment = document.Attachments.FirstOrDefault();

                if (attachment is not null)
                {
                    document.ImageSource = attachment.ImageSource;
                }

                Documents[i] = document;
            }
        }

        private void FilterDocuments(string content)
        {
            Documents.Clear();
            FullDocuments.Where(x => x.Name.Contains(content)).ForEach(Documents.Add);
        }
    }
}