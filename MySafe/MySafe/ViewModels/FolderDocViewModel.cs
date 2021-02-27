using MediatR;
using MySafe.ViewModels.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using MySafe.Mediator.Documents.GetDocumentInfo;
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
            _loadedCommand ??= new DelegateCommand(ActionAfterLoadPage);
        }

        private async void ActionAfterLoadPage()
        {
            Files.Clear();
            var id = (int) _parameters["id"];
            
            var queryResponse = await _mediator.Send(new DocumentInfoQuery(_jwtToken, id));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }

            queryResponse.Attachments.ForEach(x =>
            {
                try
                {

                    var reg = new Regex(".*base64,");
                    var base64 = reg.Replace(x.Preview, "").Replace("\\n", "");
                    var @byte = Convert.FromBase64String(base64);
                    
                    var imageSource = ImageSource.FromStream(() =>
                    {
                        Stream stream = new MemoryStream(@byte);
                        return stream;
                    });
                    Files.Add(new File() { Id = x.Id, Base64 = imageSource });
                }
                catch (Exception ex)
                {

                }
            });
        }
    }
}
