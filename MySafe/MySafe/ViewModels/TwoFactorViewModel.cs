using MediatR;
using MySafe.Core.Commands;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Navigation;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand;

namespace MySafe.Presentation.ViewModels
{
    public class TwoFactorViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private AsyncCommand _signInCommand;

        public string Code { get; set; }

        public TwoFactorViewModel(INavigationService navigationService, IMediator mediator)
            :base(navigationService)
        {
            _mediator = mediator;
        }

        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            var response = await _mediator.Send(new TwoFactorAuthenticationCommand(Code));

            if (response.HasError)
            {
                Error = response.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(MainPage));
        });
    }
}
