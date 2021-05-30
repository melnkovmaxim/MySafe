using System.Threading.Tasks;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Interfaces.Repositories;
using MySafe.Core.Interfaces.Services;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Extensions;
using MySafe.Services.Mediator.Users.SignInCommand;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class SignInViewModel : ViewModelBase, INavigatedAware
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public SignInViewModel(INavigationService navigationService, IMediator mediator, IAuthService authService)
            : base(navigationService)
        {
            _mediator = mediator;
            _authService = authService;

            SignInCommand = new AsyncCommand(SignInCommandTask);
            MoveToRegisterPage = new AsyncCommand(async () => await _navigationService.NavigateAsync(nameof(RegisterPage)));
        }

        public AsyncCommand SignInCommand { get; }
        public AsyncCommand MoveToRegisterPage { get; }

        public string Login { get; set; }
        public string Password { get; set; }

        private async Task SignInCommandTask()
        {
            var response = await _mediator.Send(new SignInCommand(Login, Password));

            if (response.HasError)
            {
                if (response.Error == "code_already_sent")
                {
                    var twoFactorToken =
                        await Ioc.Resolve<ISecureStorageRepository>().GetTwoFactorSecurityTokenAsync();
                    if (!twoFactorToken.IsExpired()) await _navigationService.NavigateAsync(nameof(TwoFactorPage));
                }

                Error = response.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(TwoFactorPage));
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await _authService.SignOut();
        }
    }
}