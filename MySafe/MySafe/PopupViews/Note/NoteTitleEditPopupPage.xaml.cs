using System;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace MySafe.Presentation.PopupViews.Note
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteTitleEditPopupPage: PopupPage
    {
        private string _noteTitle;

        public NoteTitleEditPopupPage(ref string noteTitle)
        {
            InitializeComponent();
            _noteTitle = noteTitle;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            _noteTitle = edTitle.Text;
        }
    }
}