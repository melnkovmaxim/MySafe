using Prism.Navigation;
using Xamarin.Forms;
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
            Navigation.RemovePage(this);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
