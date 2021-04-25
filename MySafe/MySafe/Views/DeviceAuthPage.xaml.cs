using System.Linq;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceAuthPage : ContentPage, INavigationAware
    {
        public DeviceAuthPage()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                while (true) await _spinnetImage.RelRotateTo(360, 3000);
            });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Navigation.NavigationStack.Reverse().Skip(1).ToList().ForEach(Navigation.RemovePage);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}