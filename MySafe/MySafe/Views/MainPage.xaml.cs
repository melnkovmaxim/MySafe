using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.SignIn;
using Prism;
using Prism.Behaviors;
using Prism.Commands;
using Prism.DryIoc;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Views
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
    }
}