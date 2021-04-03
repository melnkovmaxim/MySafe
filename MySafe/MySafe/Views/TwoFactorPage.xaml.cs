using System.Linq;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TwoFactorPage : ContentPage, INavigatedAware
    {
        public TwoFactorPage()
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