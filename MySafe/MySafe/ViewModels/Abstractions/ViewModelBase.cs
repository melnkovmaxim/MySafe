using Prism.Navigation;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses.Abstractions;
using BindableBase = Prism.Mvvm.BindableBase;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class ViewModelBase : BindableBase
    {
        protected readonly INavigationService _navigationService;
        public string Error { get; set; }

        protected ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
