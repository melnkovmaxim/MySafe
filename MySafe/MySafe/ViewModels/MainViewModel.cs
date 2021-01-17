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
using Xamarin.Forms;

namespace MySafe.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        public MainViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
        }
    }
}
