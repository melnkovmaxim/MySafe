using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
using MySafe.Presentation.Models;
using MySafe.Presentation.Views;
using MySafe.Services.Extensions;
using MySafe.Services.Mediator.Users.RefreshTokenQuery;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class AuthorizedViewModelBase : ViewModelBase, INavigatedAware
    {
        private readonly IAuthService _authService;
        private CancellationTokenSource _cancellationTokenSource;
        protected NavigationParameter _navigationParameter;

        protected AuthorizedViewModelBase(INavigationService navigationService, IAuthService authService)
            : base(navigationService)
        {
            _authService = authService;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            _cancellationTokenSource.Cancel();
        }

        public virtual async void OnNavigatedTo(INavigationParameters parameters)
        {
            _ = parameters.TryGetValue(nameof(NavigationParameter), out _navigationParameter);

            var isAuthorized = await _authService.IsAuthorized();

            if (isAuthorized == false)
            {
                await _navigationService.NavigateAsync(nameof(SignInPage));
                return;
            }

            DoAfterNavigatedTo();
        }

        protected virtual void DoAfterNavigatedTo() {}

        protected CancellationToken GetCancellationToken()
        {
            if (_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource = new CancellationTokenSource();

            return _cancellationTokenSource.Token;
        }
    }
}