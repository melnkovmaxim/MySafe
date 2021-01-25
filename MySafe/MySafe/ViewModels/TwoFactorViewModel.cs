using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Extensions;
using MySafe.Helpers;
using MySafe.Mediator.SignIn;
using MySafe.Mediator.SignInTwoFactor;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class TwoFactorViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        public string Code { get; set; }

        public TwoFactorViewModel(INavigationService navigationService, IMediator mediator) : base(navigationService)
        {
            _mediator = mediator;
        }

        public AsyncCommand _signInCommand;
        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            _jwtToken = await _mediator.Send(new TwoFactorCommand(Code, _jwtToken));
            await NavigateHelper.NavigateAsync(_navigationService, nameof(MainPage), _navigationParams);
        }, () => true, allowMultipleExecution: false);
    }
}
