using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using MySafe.Repositories.Abstractions;
using NetStandardCommands;
using Prism.Navigation;
using Xamarin.Essentials;
using BindableBase = Prism.Mvvm.BindableBase;

namespace MySafe.ViewModels.Abstractions
{
    public abstract class ViewModelBase : BindableBase
    {
        public string Error { get; set; }

        protected virtual bool IsValidToken(JwtSecurityToken jwtToken)
        {
            if (jwtToken?.ValidTo.ToUniversalTime() > DateTime.UtcNow.AddMinutes(5))
            {
                return true;
            }

            return false;
        }
    }
}
