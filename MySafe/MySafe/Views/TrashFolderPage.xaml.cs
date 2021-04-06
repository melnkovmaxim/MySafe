using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrashFolderPage : ContentPage
    {
        public TrashFolderPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            Task.Run(async () =>
            {
                while (true) await _spinnetImage.RelRotateTo(360, 3000);
            });
        }
    }
}