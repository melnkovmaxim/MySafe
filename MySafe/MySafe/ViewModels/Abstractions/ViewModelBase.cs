using System.IdentityModel.Tokens.Jwt;
using Prism.Mvvm;
using Prism.Navigation;

namespace MySafe.ViewModels.Abstractions
{
    public abstract class ViewModelBase : BindableBase, INavigatedAware
    {
        protected INavigationService _navigationService { get; }

        protected JwtSecurityToken _jwtToken;
        protected NavigationParameters _navigationParams
        {
            get
            {
                var @params = new NavigationParameters();
                @params.Add(nameof(JwtSecurityToken), _jwtToken);
                return @params;
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _jwtToken = (JwtSecurityToken) parameters[nameof(JwtSecurityToken)];
        }
    }
}
