using System.Threading.Tasks;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Domain.Repositories;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Extensions;
using MySafe.Services.Mediator.Users.SignInCommand;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;


        public SignInViewModel(INavigationService navigationService, IMediator mediator)
            : base(navigationService)
        {
            _mediator = mediator;

            SignInCommand = new AsyncCommand(SignInCommandTask);
            MoveToRegisterPage =
                new AsyncCommand(async () => await _navigationService.NavigateAsync(nameof(RegisterPage)));
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
                        await Ioc.Resolve<ISecureStorageRepository>().GetJwtSecurityTokenTwoFactorAsync();
                    if (twoFactorToken.IsValidToken()) await _navigationService.NavigateAsync(nameof(TwoFactorPage));
                }

                Error = response.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(TwoFactorPage));
        }
    }
}