using System.IdentityModel.Tokens.Jwt;
using MediatR;
using MySafe.Mediator.SignIn;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMediator _mediator;
        private AsyncCommand _signInCommand;

        public string Login { get; set; }
        public string Password { get; set; }


        public SignInViewModel(INavigationService navigationService, IMediator mediator)
        {
            _navigationService = navigationService;
            _mediator = mediator;
        }

        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            var response = await _mediator.Send(new SignInCommand(Login, Password));

            if (response.HasError)
            {
                Error = response.Error;
                return;
            }

            if (IsValidToken(response.JwtToken))
            {
                var @params = new NavigationParameters();
                @params.Add(nameof(JwtSecurityToken), response.JwtToken);
                await _navigationService.NavigateAsync(nameof(TwoFactorPage), @params);
            }
        });
    }
}
