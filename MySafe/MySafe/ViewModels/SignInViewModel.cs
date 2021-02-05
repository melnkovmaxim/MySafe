using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Helpers;
using MySafe.Mediator.SignIn;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using NavigationParameters = Prism.Navigation.NavigationParameters;

namespace MySafe.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        public string Login { get; set; }
        public string Password { get; set; }


        public SignInViewModel(INavigationService navigationService, IMediator mediator) : base(navigationService)
        {
            _mediator = mediator;
        }

        private AsyncCommand _signInCommand;
        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            /*_jwtToken = await _mediator.Send(new SignInCommand(Login, Password));*/
            
            await NavigateHelper.NavigateAsync(_navigationService, nameof(TwoFactorPage), _navigationParams);
        }, () => true, allowMultipleExecution: false);
    }
}
