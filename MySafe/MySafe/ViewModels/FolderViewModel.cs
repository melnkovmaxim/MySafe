using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Documents.CreateDocumentCommand;
using MySafe.Business.Mediator.Folders.FolderInfoQuery;
using MySafe.Business.Mediator.Safe.SafeInfoQuery;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MySafe.Presentation.ViewModels
{
    public class FolderViewModel: AuthorizedRefreshViewModel<Folder>
    {
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

        private string _filter;

        public ObservableCollection<Document> Documents { get; set; }
        private List<Document> DocumentsList { get; set; }

        private readonly IMediator _mediator;
        private AsyncCommand<Document> _moveToDocumentCommand;
        private AsyncCommand _addDocumentCommand;

        private readonly Dictionary<string, string> _parentsIconsDictionary = new Dictionary<string, string>
        {
            {"Документы", "docs.png"},
            {"Квартира, Машина, Дача", "auto.png"},
            {"Налоги, Аренда, Платежи", "tax.png"},
            {"Здоровье", "health.png"},
            {"ЖКХ", "util.png"}
        };

        public string IconPath { get; set; }

        public FolderViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
            Documents = new ObservableCollection<Document>();
        }

        public AsyncCommand<Document> MoveToDocumentCommand => _moveToDocumentCommand 
            ??= new AsyncCommand<Document>(async (document) =>
        {
            var @params = GetItemNaviigationParams(document.Id, document.Name);
            await _navigationService.NavigateAsync(nameof(DocumentPage), @params);
        });

        public AsyncCommand AddDocumentCommand => _addDocumentCommand ??= new AsyncCommand(async () =>
        {  
            bool answer = await Application.Current.MainPage.DisplayAlert ("Создать новый документ?", null, "Да", "Нет");
            if (!answer) return;

            var response = await _mediator.Send(new CreateDocumentCommand(folderId));

            if (response.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось создать документ, что-то пошло не так... ", "Ok");
            }

            var mediatorResult = await _refreshTask;
            RefillObservableCollection(mediatorResult);
        });

        protected override Task<Folder> _refreshTask => _mediator.Send(new FolderInfoQuery(_itemId.Value));
        protected override async void RefillObservableCollection(Folder mediatorResponse)
        {
            var isSuccess = _parentsIconsDictionary.TryGetValue(_itemName, out var iconPath);
            IconPath = isSuccess ? iconPath : "other.png";

            DocumentsList = mediatorResponse.Documents;
            Documents.Clear();
            mediatorResponse.Documents.ForEach(Documents.Add);

            
            var safeFolders = await _mediator.Send(new SafeInfoQuery());
            var currentFolder = safeFolders?.Folders.FirstOrDefault(x => x.Id == mediatorResponse.Id);
            FolderName = currentFolder?.Name.Split(":").FirstOrDefault();
            folderId = currentFolder?.Id ?? int.MinValue;
        }
    }
}
