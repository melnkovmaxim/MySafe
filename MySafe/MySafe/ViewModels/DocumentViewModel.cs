using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.SignIn;
using MySafe.ViewModels.Abstractions;
using Prism.Commands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class DocumentViewModel : AuthorizedViewModelBase
    {
        private readonly IMediator _mediator;

        public DocumentViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
        }

        //public DelegateCommand LoadedCommand => new DelegateCommand(async () =>
        //    {
        //        var result = await _mediator.Send(new SignInCommand("", "password"));
        //    });
    }
}
