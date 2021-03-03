using MySafe.Core;
using MySafe.Presentation.Views;
using Prism.Commands;
using Prism.Navigation;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Extensions;
using MySafe.Data.Abstractions;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class AuthorizedViewModelBase : ViewModelBase, INavigatedAware
    {
        protected JwtSecurityToken _jwtToken;
        protected INavigationParameters _parameters;
        protected int? _itemId;
        protected string _itemName;
        private DelegateCommand _loadedCommand { get; }

        protected AuthorizedViewModelBase(INavigationService navigationService) 
            : base(navigationService)
        {
            _loadedCommand ??= new DelegateCommand(ActionAfterLoadPage);
        }

        protected abstract void ActionAfterLoadPage();

        protected NavigationParameters GetItemNaviigationParams(int itemId, string itemName)
        {
            return new NavigationParameters()
            {
                { MySafeApp.Resources.ItemId, itemId },
                { MySafeApp.Resources.ItemName, itemName }
            };
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add(nameof(MySafeApp.Resources.ItemId), _itemId);
            parameters.Add(nameof(MySafeApp.Resources.ItemName), _itemName);
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            _parameters = parameters;

            _itemId = (int?) parameters[nameof(MySafeApp.Resources.ItemId)];
            _itemName = (string) parameters[nameof(MySafeApp.Resources.ItemName)];

            _jwtToken ??= (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];
            _jwtToken ??= await Ioc.Resolve<ISecureStorageRepository>()
                .GetJstTokenAsync();

            if (!_jwtToken.IsValidToken())
            {
                await _navigationService.NavigateAsync(nameof(SignInPage));
                return;
            }

            _loadedCommand.Execute();
        }
    }
}
