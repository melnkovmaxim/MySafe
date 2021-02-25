using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.FolderInfo;
using MySafe.Mediator.SignIn;
using MySafe.Models;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using Prism.Commands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class DocumentViewModel : AuthorizedViewModelBase
    {
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
            var id = (int) _parameters["id"];
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
