using System;
using System.Runtime.CompilerServices;
using DryIoc;
using Prism;
using Prism.Ioc;
using MySafe.ViewModels;
using MySafe.Views;
using Prism.DryIoc;
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

            var password = await SecureStorage.GetAsync("ApplicationPassword");

            if (string.IsNullOrEmpty(password))
            {
                await NavigationService.NavigateAsync($"NavigationPage/{nameof(RegisterPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"NavigationPage/{nameof(LoginPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
        }
    }
}
