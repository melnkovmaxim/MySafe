using MySafe.Presentation.Extensions;
using MySafe.Presentation.Repositories.Abstractions;
using MySafe.Presentation.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "Roboto-Medium")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Roboto-Regular")]
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

            var token = await Ioc.Resolve<ISecureStorageRepository>().GetJstTokenAsync();
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
