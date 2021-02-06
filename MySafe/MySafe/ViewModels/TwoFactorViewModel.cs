using System.IdentityModel.Tokens.Jwt;
using MediatR;
using MySafe.Mediator.SignInTwoFactor;
using MySafe.Repositories.Abstractions;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class TwoFactorViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IMediator _mediator;
        private JwtSecurityToken _tempToken;
        private AsyncCommand _signInCommand;

        public string Code { get; set; }

        public TwoFactorViewModel(INavigationService navigationService, IMediator mediator)
        {
            _navigationService = navigationService;
            _mediator = mediator;
        }

        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            var response = await _mediator.Send(new TwoFactorCommand(Code, _tempToken));

            if (response.HasError)
            {
                Error = response.Error;
                return;
            }

            if (IsValidToken(_tempToken))
            {
                await _navigationService.NavigateAsync(nameof(MainPage));
            }
        });

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _tempToken = (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];
        }
    }
}
