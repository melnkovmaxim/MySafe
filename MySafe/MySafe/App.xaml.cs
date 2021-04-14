using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
using MySafe.Presentation.Views;
using MySafe.Services.Extensions;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "Roboto-Medium")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Roboto-Regular")]

namespace MySafe.Presentation
{
    public partial class App
    {
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            AppCenter.Start("android=a4c8c5c8-6ac7-43cb-8de1-ffd30fcd0318;",
                typeof(Analytics), typeof(Crashes));

            var isLoggedOut = await Ioc.Resolve<IAuthService>().IsAuthorized();
            var startPage = isLoggedOut ? nameof(SignInPage) : nameof(DeviceAuthPage);
            await NavigationService.NavigateAsync($"NavigationPage/{startPage}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.AddApplication()
                .AddNavigation()
                .AddMapper()
                .AddRepositories()
                .AddServices()
                .AddMediatr();
        }
    }
}