using System.Threading.Tasks;
using MediatR;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Users.SignInCommand;
using MySafe.Business.Services.Abstractions;
using MySafe.Core.Commands;
using MySafe.Data.Abstractions;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        public AsyncCommand SignInCommand { get; }

        public string Login { get; set; }
        public string Password { get; set; }


        public SignInViewModel(INavigationService navigationService, IMediator mediator)
            :base(navigationService)
        {
            _mediator = mediator;

            SignInCommand = new AsyncCommand(SignInCommandTask);
        }

        private async Task SignInCommandTask()
        {
            var response = await _mediator.Send(new SignInCommand(Login, Password));

            if (response.HasError)
            {
                if (response.Error == "code_already_sent")
                {
                    var twoFactorToken = await Ioc.Resolve<ISecureStorageRepository>().GetJwtSecurityTokenTwoFactorAsync();
                    if (twoFactorToken.IsValidToken()) await _navigationService.NavigateAsync(nameof(TwoFactorPage));

                }
                Error = response.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(TwoFactorPage));
        }
    }
}
