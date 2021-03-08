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
    public class FolderViewModel: AuthorizedViewModelBase
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

        public FolderViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;

            Documents = new ObservableCollection<Document>();
        }


        protected override async Task ActionAfterLoadPage()
        {
            var queryResponse = await _mediator.Send(new FolderInfoQuery(_jwtToken.RawData, _itemId.Value));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }

            DocumentsList = queryResponse.Documents;
            Documents.Clear();
            queryResponse.Documents.ForEach(Documents.Add);

            
            var safeFolders = await _mediator.Send(new SafeInfoQuery(_jwtToken.RawData));
            var currentFolder = safeFolders?.Folders.FirstOrDefault(x => x.Id == queryResponse.Id);
            FolderName = currentFolder?.Name.Split(":").FirstOrDefault();
            folderId = currentFolder?.Id ?? int.MinValue;
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

            var response = await _mediator.Send(new CreateDocumentCommand(_jwtToken.RawData, folderId));

            if (response.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось создать документ, что-то пошло не так... ", "Ok");
            }

            ActionAfterLoadPage();
        });
    }
}
