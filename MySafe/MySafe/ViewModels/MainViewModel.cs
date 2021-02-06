using MediatR;
using MySafe.Mediator.SignOut;
using MySafe.Services.Abstractions;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class MainViewModel : AuthorizedViewModelBase
    {
        private readonly IMediator _mediator;
        private AsyncCommand _signOutCommand;

        public MainViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
        }

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            _ = _mediator.Send(new SignOutCommand(_jwtToken));
            await _navigationService.NavigateAsync(nameof(AuthPage));
        });
    }
}
