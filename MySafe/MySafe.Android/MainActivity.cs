using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using DryIoc;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
using MySafe.Droid.Repositories;
using MySafe.Droid.Services;
using MySafe.Presentation;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
using Plugin.Printing;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Auth.Presenters.XamarinAndroid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace MySafe.Droid
{
    [Activity(Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                               ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Forms.SetFlags("CarouselView_Experimental", "Shapes_Experimental");


            RequestedOrientation = ScreenOrientation.Portrait;

            PrintServiceAndroidHelper.ActivityInstance = this;
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);
            AuthenticationConfiguration.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentOnUnhandledException;

            LoadApplication(new App(new AndroidInitializer()));
            var container = PrismApplicationBase.Current.Container.GetContainer();

            container.Register<IStoragePathesRepository, StoragePathesRepository>();
            container.Register<IPrintService, PrintService>();
        }

        private void AndroidEnvironmentOnUnhandledException(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}