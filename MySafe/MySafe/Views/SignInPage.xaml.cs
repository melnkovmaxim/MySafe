using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage, INavigationAware
    {
        public SignInPage()
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