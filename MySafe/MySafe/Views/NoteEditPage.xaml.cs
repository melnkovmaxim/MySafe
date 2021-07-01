using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEditPage : ContentPage
    {
        public NoteEditPage()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}