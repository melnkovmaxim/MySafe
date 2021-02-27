using MediatR;
using MySafe.ViewModels.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using MySafe.Mediator.Documents.GetDocumentInfo;
using MySafe.Mediator.Folders.GetFolderInfo;
using MySafe.Models;
using Xamarin.Forms;
using File = MySafe.Models.File;

namespace MySafe.ViewModels
{
    public class FolderDocViewModel : AuthorizedViewModelBase
    {
        private readonly IMediator _mediator;
        public ObservableCollection<File> Files { get; set; }

        public FolderDocViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
            Files = new ObservableCollection<File>();
        }

        protected override async void ActionAfterLoadPage()
        {
            //Documents.Clear();
            //var id = (_parameters["id"] == null || !(_parameters["id"] is int))  ? ID : (int) _parameters["id"];
            //ID = id;
            //var queryResponse = await _mediator.Send(new FolderInfoQuery(_jwtToken, id));

            //if (queryResponse.HasError)
            //{
            //    Error = queryResponse.Error;
            //    return;
            //}

            //queryResponse.Documents.ForEach(x =>
            //{
            //    Documents.Add(new Document(_navigationService) { Id = x.Id, Name = x.Name, ContainsAttachments = x.ContainsAttachments});
            //});
        }
    }
}
