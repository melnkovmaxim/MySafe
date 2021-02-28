using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.Documents.GetDocumentInfo;
using MySafe.Mediator.Folders.GetFolderInfo;
using MySafe.Models.Responses;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;
using Xamarin.Forms;
using Document = MySafe.Models.Document;

namespace MySafe.ViewModels
{
    public class FolderViewModel: AuthorizedViewModelBase
    {
        public static int ID { get; set; }
        public ObservableCollection<DocumentResponse> Documents { get; set; }

        private readonly IMediator _mediator;
        private AsyncCommand<DocumentResponse> _moveToDocumentCommand;

        public FolderViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;

            Documents = new ObservableCollection<DocumentResponse>();
        }


        protected override async void ActionAfterLoadPage()
        {
            var queryResponse = await _mediator.Send(new FolderInfoQuery(_jwtToken, _itemId.Value));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }
            
            Documents.Clear();
            queryResponse.Documents.ForEach(Documents.Add);
        }

        public AsyncCommand<DocumentResponse> MoveToDocumentCommand => _moveToDocumentCommand 
            ??= new AsyncCommand<DocumentResponse>(async (document) =>
        {
            var @params = GetItemNaviigationParams(document.Id, document.Name);
            await _navigationService.NavigateAsync(nameof(DocumentPage), @params);
        });
    }
}
