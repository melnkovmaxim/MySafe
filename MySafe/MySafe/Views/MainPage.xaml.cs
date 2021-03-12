using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ImTools;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MySafe.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            RefreshView_OnRefreshing(null, null);
        }

        private async void RefreshView_OnRefreshing(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                while (_spinnetImage.IsVisible)
                {
                    await _spinnetImage.RelRotateTo(360, 1000); 
                }
            });
        }
    }
}