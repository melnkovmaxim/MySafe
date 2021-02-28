using System;
using MediatR;
using MySafe.Models;
using MySafe.ViewModels.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySafe.Core;
using MySafe.Mediator.Documents.GetDocumentInfo;
using MySafe.Mediator.Folders.GetFolderInfo;
using MySafe.Mediator.Sheets.GetFile;
using MySafe.Mediator.Sheets.UploadFile;
using MySafe.Models.Responses;
using NetStandardCommands;
using Plugin.DownloadManager;
using Xamarin.Forms;
using File = MySafe.Models.File;

namespace MySafe.ViewModels
{
    public class DocumentViewModel : AuthorizedViewModelBase
    {
        public static int ID { get; set; }
        private readonly IMediator _mediator;
        private AsyncCommand<Attachment> _downloadFileCommand;

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

            Document = queryResponse;
            Attachments.Clear();
            queryResponse.Attachments.ForEach(Attachments.Add);
        }

        public AsyncCommand<Attachment> DownloadFileCommand => 
            _downloadFileCommand ??= new AsyncCommand<Attachment>(async  (attachment) =>
        {   
            var queryResponse = await _mediator.Send(new FileQuery(_jwtToken, attachment.Id));
            var queryResponse2 = await _mediator.Send(new UploadFileCommand(_jwtToken, Document.Id, attachment.Name, queryResponse));

            var k = 0;
        });
    }
}
