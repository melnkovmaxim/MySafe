using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core;
using MySafe.Repositories.Abstractions;
using MySafe.Views;
using Prism.Commands;
using Prism.Navigation;

namespace MySafe.ViewModels.Abstractions
{
    public abstract class AuthorizedViewModelBase : ViewModelBase, INavigatedAware
    {
        protected JwtSecurityToken _jwtToken;
        protected INavigationParameters _parameters;
        protected int? _itemId;
        protected string? _itemName;
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
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            _parameters = parameters;

            _itemId = (int?) parameters[nameof(MySafeApp.Resources.ItemId)];
            _itemName = (string?) parameters[nameof(MySafeApp.Resources.ItemName)];

            _jwtToken ??= (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];
            _jwtToken ??= await Ioc.Resolve<ISecureStorageRepository>()
                .GetJstTokenAsync();

            if (!IsValidToken(_jwtToken))
            {
                await _navigationService.NavigateAsync(nameof(SignInPage));
                return;
            }

            _loadedCommand.Execute();
        }
    }
}
