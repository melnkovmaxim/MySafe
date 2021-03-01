using System.Linq;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MySafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage, INavigationAware
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Navigation.NavigationStack.ToList().ForEach(Navigation.RemovePage);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
