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
        }

        private void Blue_Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.Blue;
        }

        private void RefreshView_OnRefreshing(object sender, EventArgs e)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            Task.Run(async () =>
            {
                while (token.IsCancellationRequested == false)
                {
                    await _spinnetImage.RelRotateTo(360, 1000); 
                }
            }, token);

            cancellationTokenSource.Cancel();
        }
    }
}