using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Helpers;
using MySafe.Mediator.SignIn;
using MySafe.Mediator.SignOut;
using MySafe.Services.Abstractions;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private AsyncCommand _signOutCommand;

        public MainViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
        }

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            _ = _mediator.Send(new SignOutCommand(_jwtToken));
            await Ioc.Resolve<IDeviceAuthService>().Logout();
            await NavigateHelper.NavigateAsync(_navigationService, nameof(AuthPage));
        });
    }
}
