using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Models.Responses;
using MySafe.Domain.Services;
using MySafe.Presentation.EntityExtensions;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Documents.CreateDocumentCommand;
using MySafe.Services.Mediator.Folders.FolderInfoQuery;
using MySafe.Services.Mediator.Safe.SafeInfoQuery;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MySafe.Presentation.ViewModels
{
    public class FolderViewModel : AuthorizedRefreshViewModel<FolderEntity, Folder>
    {
        private readonly IMediator _mediator;

        private readonly Dictionary<string, string> _parentsIconsDictionary = new Dictionary<string, string>
        {
            {"Документы", "docs.png"},
            {"Квартира, Машина, Дача", "auto.png"},
            {"Налоги, Аренда, Платежи", "tax.png"},
            {"Здоровье", "health.png"},
            {"ЖКХ", "util.png"}
        };

        private AsyncCommand _addDocumentCommand;

        private string _filter;
        private AsyncCommand<Document> _moveToDocumentCommand;

        public FolderViewModel(INavigationService navigationService, IMapper mapper, IMediator mediator, IAuthService authService)
            : base(navigationService, mapper, authService)
        {
            _mediator = mediator;
            Documents = new ObservableCollection<Document>();
        }

        public string FolderName { get; set; }
        private int folderId { get; set; }

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                Documents.Clear();
                DocumentsList.Where(x => x.Name.Contains(value)).ForEach(Documents.Add);
            }
        }

        public ObservableCollection<Document> Documents { get; set; }
        private List<Document> DocumentsList { get; set; }

        public string IconPath { get; set; }

        // _navigationParameter.ChildId
        public AsyncCommand<Document> MoveToDocumentCommand => _moveToDocumentCommand
            ??= new AsyncCommand<Document>(async document =>
            {
                var @params = new NavigationParameters() { { nameof(NavigationParameter), new NavigationParameter(document.Id, document.Name) }};
                await _navigationService.NavigateAsync(nameof(DocumentPage), @params);
            });

        public AsyncCommand AddDocumentCommand => _addDocumentCommand ??= new AsyncCommand(async () =>
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Создать новый документ?", null, "Да", "Нет");
            if (!answer) return;

            var response = (await _mediator.Send(new CreateDocumentCommand(folderId))).ToDocumentToPresentationModel();

            if (response.HasError)
                await Application.Current.MainPage.DisplayAlert("Ошибка",
                    "Не получилось создать документ, что-то пошло не так... ", "Ok");

            var mediatorResult = (await _refreshTask).ToFolderPresentationModel();
            RefillObservableCollection(mediatorResult);
        });

        protected override Task<FolderEntity> _refreshTask => _mediator.Send(new FolderInfoQuery(_navigationParameter.ChildId));

        protected override async void RefillObservableCollection(Folder mediatorEntity)
        {
            var isSuccess = _parentsIconsDictionary.TryGetValue(_navigationParameter.ChildName, out var iconPath);
            IconPath = isSuccess ? iconPath : "other.png";

            DocumentsList = mediatorEntity.Documents;
            Documents.Clear();
            mediatorEntity.Documents.ForEach(Documents.Add);


            var safeFolders = await _mediator.Send(new SafeInfoQuery());
            var currentFolder = safeFolders?.Folders.FirstOrDefault(x => x.Id == mediatorEntity.Id);
            FolderName = currentFolder?.Name.Split(":").FirstOrDefault().Split(",").FirstOrDefault();
            folderId = currentFolder?.Id ?? int.MinValue;
        }
    }
}