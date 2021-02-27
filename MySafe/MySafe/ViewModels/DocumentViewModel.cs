using System;
using MediatR;
using MySafe.Models;
using MySafe.ViewModels.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using MySafe.Mediator.Documents.GetDocumentInfo;
using MySafe.Mediator.Folders.GetFolderInfo;
using MySafe.Models.Responses;
using Xamarin.Forms;
using File = MySafe.Models.File;

namespace MySafe.ViewModels
{
    public class DocumentViewModel : AuthorizedViewModelBase
    {
        public static int ID { get; set; }
        private readonly IMediator _mediator;

        public DocumentResponse Document { get; set; }
        public ObservableCollection<Attachment> Attachments { get; set; }

        public DocumentViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
            Attachments = new ObservableCollection<Attachment>();
        }

        protected override async void ActionAfterLoadPage()
        {          
            var queryResponse = await _mediator.Send(new DocumentInfoQuery(_jwtToken, _itemId.Value));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }

            Attachments.Clear();
            queryResponse.Attachments.ForEach(Attachments.Add);
        }
    }
}
