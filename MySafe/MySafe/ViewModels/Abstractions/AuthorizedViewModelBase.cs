using System.Threading;
using System.Threading.Tasks;
using MySafe.Core;
using MySafe.Domain.Repositories;
using MySafe.Presentation.Views;
using MySafe.Services.Extensions;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class AuthorizedViewModelBase : ViewModelBase, INavigatedAware
    {
        private CancellationTokenSource _cancellationTokenSource;
        protected int? _itemId;
        protected string _itemName;
        protected INavigationParameters _parameters;

        protected AuthorizedViewModelBase(INavigationService navigationService)
            : base(navigationService)
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            _cancellationTokenSource.Cancel();
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            SaveParameters(parameters);
            var isNavigated = await TryNavigateToSignInPage();
            if (!isNavigated) DoAfterNavigatedTo();
        }

        protected NavigationParameters GetItemNaviigationParams(int itemId, string itemName)
        {
            return new NavigationParameters
            {
                {MySafeApp.Resources.ItemId, itemId},
                {MySafeApp.Resources.ItemName, itemName}
            };
        }

        protected CancellationToken GetCancellationToken()
        {
            if (_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource = new CancellationTokenSource();

            return _cancellationTokenSource.Token;
        }

        protected virtual void DoAfterNavigatedTo()
        {
        }

        protected void SaveParameters(INavigationParameters parameters)
        {
            _parameters = parameters;

            _itemId = (int?) parameters[nameof(MySafeApp.Resources.ItemId)];
            _itemName = (string) parameters[nameof(MySafeApp.Resources.ItemName)];
        }

        protected async Task<bool> TryNavigateToSignInPage()
        {
            var jwtToken = await Ioc.Resolve<ISecureStorageRepository>()
                .GetJwtSecurityTokenAsync();

            if (!jwtToken.IsValidToken())
            {
                await _navigationService.NavigateAsync(nameof(SignInPage));
                return true;
            }

            return false;
        }
    }
}