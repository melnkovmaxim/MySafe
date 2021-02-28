using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.Documents.CreateDocument;
using MySafe.Mediator.Documents.GetDocumentInfo;
using MySafe.Mediator.Folders.GetFolderInfo;
using MySafe.Mediator.Safe.SafeInfo;
using MySafe.Models.Responses;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MySafe.ViewModels
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

        public ObservableCollection<DocumentResponse> Documents { get; set; }
        private List<DocumentResponse> DocumentsList { get; set; }

        private readonly IMediator _mediator;
        private AsyncCommand<DocumentResponse> _moveToDocumentCommand;
        private AsyncCommand _addDocumentCommand;

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

            DocumentsList = queryResponse.Documents;
            Documents.Clear();
            queryResponse.Documents.ForEach(Documents.Add);

            
            var safeFolders = await _mediator.Send(new SafeInfoQuery(_jwtToken));
            var currentFolder = safeFolders?.Folders.FirstOrDefault(x => x.Id == queryResponse.Id);
            FolderName = currentFolder?.Name;
            folderId = currentFolder?.Id ?? int.MinValue;
        }

        public AsyncCommand<DocumentResponse> MoveToDocumentCommand => _moveToDocumentCommand 
            ??= new AsyncCommand<DocumentResponse>(async (document) =>
        {
            var @params = GetItemNaviigationParams(document.Id, document.Name);
            await _navigationService.NavigateAsync(nameof(DocumentPage), @params);
        });

        public AsyncCommand AddDocumentCommand => _addDocumentCommand ??= new AsyncCommand(async () =>
        {  
            bool answer = await Application.Current.MainPage.DisplayAlert ("Создать новый документ?", null, "Да", "Нет");
            if (!answer) return;

            var response = await _mediator.Send(new CreateDocumentCommand(_jwtToken, folderId));

            if (response.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось создать документ, что-то пошло не так... ", "Ok");
            }

            ActionAfterLoadPage();
        });
    }
}
