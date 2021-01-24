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
using NetStandardCommands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using NavigationParameters = Prism.Navigation.NavigationParameters;

namespace MySafe.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }


        public SignInViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private AsyncCommand _signInCommand;
        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            var jwtToken = await Task.Run(() => Ioc.Resolve<IMediator>()
                    .Send(new SignInCommand(Login, Password))).ConfigureAwait(false);

            var navigationParams = new NavigationParameters();
            navigationParams.Add(nameof(JwtSecurityToken), jwtToken);

            await NavigateHelper.NavigateAsync(_navigationService, nameof(TwoFactorViewModel), navigationParams);
        }, () => true, !SignInCommand.IsExecuting);
    }
}
