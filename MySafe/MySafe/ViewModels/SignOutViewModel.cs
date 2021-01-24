using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Helpers;
using MySafe.Mediator.SignOut;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class SignOutViewModel : ViewModelBase, INavigatedAware
    {
        private readonly IMediator _mediator;
        private JwtSecurityToken _jwtToken;
        private AsyncCommand _signOutCommand;

        public SignOutViewModel(INavigationService navigationService, IMediator mediator) : base(navigationService)
        {
            _mediator = mediator;
        }

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            _ = _mediator.Send(new SignOutCommand(_jwtToken));
            await NavigateHelper.NavigateAsync(_navigationService, nameof(AuthPage));
        }, () => true, !SignOutCommand.IsExecuting);

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _jwtToken = (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];
        }
    }
}
