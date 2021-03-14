using System;
using System.Net;
using System.Net.Mail;
using MediatR;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Trash.TrashContentQuery;
using MySafe.Data.Abstractions;
using MySafe.Presentation.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "Roboto-Medium")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Roboto-Regular")]
namespace MySafe.Presentation
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

            try
            {
                AppCenter.Start("android=a4c8c5c8-6ac7-43cb-8de1-ffd30fcd0318;",
                    typeof(Analytics), typeof(Crashes));
            }
            catch(Exception ex)
            {
                try
                {
                    var from = new MailAddress("admin@justgarbage.ru", "admin");
                    var to = new MailAddress("melnikovmaxim.nhk@gmail.com");
                    var message = new MailMessage(from, to);
                    message.Subject = "AppCenter.Start exception";
                    message.Body = ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace;
                    var smtp = new SmtpClient("mail.justgarbage.ru", 25);
                    smtp.Credentials = new NetworkCredential("admin@justgarbage.ru", "Ssd17xDldD");
                    await smtp.SendMailAsync(message);
                }
                catch{}
            }

            var token = await Ioc.Resolve<ISecureStorageRepository>().GetJwtSecurityTokenAsync();
            var startPage = token.IsValidToken() ? nameof(DeviceAuthPage) : nameof(SignInPage);
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
