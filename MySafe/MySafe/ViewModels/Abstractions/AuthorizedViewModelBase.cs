using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Repositories.Abstractions;
using MySafe.Views;
using Prism.Navigation;

namespace MySafe.ViewModels.Abstractions
{
    public abstract class AuthorizedViewModelBase : ViewModelBase, INavigatedAware
    {
        protected readonly INavigationService _navigationService;
        protected JwtSecurityToken _jwtToken;

        protected AuthorizedViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {   
            _jwtToken = (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];

            if (IsValidToken(_jwtToken)) return;

            var token = await Ioc.Resolve<ISecureStorageRepository>().GetTokenAsync();
            _jwtToken = new JwtSecurityToken(token);

            if (!IsValidToken(_jwtToken))
            {
                await _navigationService.NavigateAsync(nameof(SignInPage));
            }
        }
    }
}
