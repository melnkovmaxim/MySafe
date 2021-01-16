using System;
using System.Runtime.CompilerServices;
using DryIoc;
using MySafe.Services;
using MySafe.Services.Abstractions;
using Prism;
using Prism.Ioc;
using MySafe.ViewModels;
using MySafe.Views;
using Prism.DryIoc;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;

[assembly: ExportFont("Montserrat-Regular.ttf", Alias= "Montserrat")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias= "Roboto-Medium")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias= "Roboto-Regular")]
namespace MySafe
{
    public partial class App
    {
        public App() : this(null) {}

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"NavigationPage/{nameof(AuthPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.AddApplication()
                .AddNavigation()
                .AddServices()
                .AddMediatr();
        }
    }
}
