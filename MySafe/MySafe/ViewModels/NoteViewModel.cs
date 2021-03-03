using MySafe.Presentation.ViewModels.Abstractions;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        public NoteViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
