using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.ViewModels.Abstractions;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
