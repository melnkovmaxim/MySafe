using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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