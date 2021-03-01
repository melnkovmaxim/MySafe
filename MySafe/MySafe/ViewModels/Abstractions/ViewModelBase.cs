using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using MySafe.Extensions;
using MySafe.Models.Responses;
using MySafe.Repositories.Abstractions;
using NetStandardCommands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Essentials;
using Xamarin.Forms;
using BindableBase = Prism.Mvvm.BindableBase;

namespace MySafe.ViewModels.Abstractions
{
    public abstract class ViewModelBase : BindableBase
    {
        protected readonly INavigationService _navigationService;
        public string Error { get; set; }

        protected ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected virtual async Task HandleResponse(IResponse response, string pageName = null, JwtSecurityToken token = null)
        {
            if (response.HasError)
            {
                Error = response.Error;
                return;
            }

            if (string.IsNullOrEmpty(pageName)) return;

            if (token.IsValidToken())
            {
                var @params = new NavigationParameters {{nameof(JwtSecurityToken), token}};
                await _navigationService.NavigateAsync(pageName, @params);
            }
        }
    }
}
