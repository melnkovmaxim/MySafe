using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.SignIn;
using Prism;
using Prism.Commands;
using Prism.DryIoc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Blue_Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.Blue;
        }
    }
}