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
        private readonly IJwtService _jwtService;
        private CancellationTokenSource _cancellationTokenSource;
        protected NavigationParameter _navigationParameter;

        protected AuthorizedViewModelBase(INavigationService navigationService, IJwtService jwtService)
            : base(navigationService)
        {
            _jwtService = jwtService;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            _cancellationTokenSource.Cancel();
        }

        public virtual async void OnNavigatedTo(INavigationParameters parameters)
        {
            _ = parameters.TryGetValue(nameof(NavigationParameter), out _navigationParameter);

            var isExpired = await _jwtService.IsExpiredJwtTokensAsync();

            if (isExpired)
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