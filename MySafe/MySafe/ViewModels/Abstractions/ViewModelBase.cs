using Prism.Mvvm;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class ViewModelBase : BindableBase
    {
        protected readonly INavigationService _navigationService;

        protected ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Error { get; set; }
    }
}