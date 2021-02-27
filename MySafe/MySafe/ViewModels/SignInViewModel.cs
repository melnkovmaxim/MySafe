using MediatR;
using MySafe.Mediator.Users.SignIn;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private AsyncCommand _signInCommand;

        public string Login { get; set; }
        public string Password { get; set; }


        public SignInViewModel(INavigationService navigationService, IMediator mediator)
            :base(navigationService)
        {
            _mediator = mediator;
        }

        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            var response = await _mediator.Send(new SignInCommand(Login, Password));
            await HandleResponse(response, nameof(TwoFactorPage), response.JwtToken);
        });
    }
}
