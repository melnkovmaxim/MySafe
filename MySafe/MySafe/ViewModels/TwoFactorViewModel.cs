using MediatR;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Mediator.Users.SignInTwoFactor;

namespace MySafe.ViewModels
{
    public class TwoFactorViewModel : ViewModelBase, INavigatedAware
    {
        private readonly IMediator _mediator;
        private JwtSecurityToken _tempToken;
        private AsyncCommand _signInCommand;

        public string Code { get; set; }

        public TwoFactorViewModel(INavigationService navigationService, IMediator mediator)
            :base(navigationService)
        {
            _mediator = mediator;
        }

        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            var response = await _mediator.Send(new TwoFactorCommand(Code, _tempToken));
            await HandleResponse(response, nameof(MainPage), response.JwtToken);
        });


        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _tempToken = (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
    }
}
