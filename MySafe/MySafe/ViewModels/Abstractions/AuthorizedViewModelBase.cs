using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Repositories.Abstractions;
using MySafe.Views;
using Prism.Commands;
using Prism.Navigation;

namespace MySafe.ViewModels.Abstractions
{
    public abstract class AuthorizedViewModelBase : ViewModelBase, INavigatedAware
    {
        protected JwtSecurityToken _jwtToken;
        protected DelegateCommand _loadedCommand;
        protected INavigationParameters _parameters;

        protected AuthorizedViewModelBase(INavigationService navigationService) 
            : base(navigationService)
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            _parameters = parameters;
            _jwtToken ??= (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];
            _jwtToken ??= await Ioc.Resolve<ISecureStorageRepository>()
                .GetJstTokenAsync();

            if (!IsValidToken(_jwtToken))
            {
                await _navigationService.NavigateAsync(nameof(SignInPage));
                return;
            }

            _loadedCommand?.Execute();
        }
    }
}
