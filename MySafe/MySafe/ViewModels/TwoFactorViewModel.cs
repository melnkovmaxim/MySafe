using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Extensions;
using MySafe.Mediator.SignIn;
using MySafe.Mediator.SignInTwoFactor;
using MySafe.ViewModels.Abstractions;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class TwoFactorViewModel : ViewModelBase, INavigatedAware
    {
        public string Code { get; set; }

        private JwtSecurityToken _jwtToken;

        public TwoFactorViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public AsyncCommand _signInCommand;
        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            await Task.Run(() => Ioc.Resolve<IMediator>().Send(new TwoFactorCommand(Code, _jwtToken)));
        }, () => true, !SignInCommand.IsExecuting);

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _jwtToken = (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];
        }
    }
}
