using Prism.Mvvm;
using Prism.Navigation;

namespace MySafe.ViewModels.Abstractions
{
    public abstract class ViewModelBase : BindableBase
    {
        protected INavigationService _navigationService { get; }

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
    }
}
