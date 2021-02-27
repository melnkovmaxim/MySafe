using MediatR;
using MySafe.Models;
using MySafe.ViewModels.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using MySafe.Mediator.Folders.GetFolderInfo;

namespace MySafe.ViewModels
{
    public class DocumentViewModel : AuthorizedViewModelBase
    {
        public static int ID { get; set; }
        private readonly IMediator _mediator;
        public ObservableCollection<Document> Documents { get; set; }

        private Document _selectedDocument;
        public Document SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                _selectedDocument = value;
            }
        }

        public DocumentViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
            Documents = new ObservableCollection<Document>();
            _loadedCommand ??= new DelegateCommand(ActionAfterLoadPage);
        }

        private async void ActionAfterLoadPage()
        {
            Documents.Clear();
            var id = (_parameters["id"] == null || !(_parameters["id"] is int))  ? ID : (int) _parameters["id"];
            ID = id;
            var queryResponse = await _mediator.Send(new FolderInfoQuery(_jwtToken, id));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }

            queryResponse.Documents.ForEach(x =>
            {
                Documents.Add(new Document(_navigationService) { Id = x.Id, Name = x.Name, ContainsAttachments = x.ContainsAttachments});
            });

            //await _navigationService.NavigateAsync(nameof(FolderDocPage), new NavigationParameters{{"id", 99}});
        }


        //public DelegateCommand LoadedCommand => new DelegateCommand(async () =>
        //    {
        //        var result = await _mediator.Send(new SignInCommand("", "password"));
        //    });
    }
}
