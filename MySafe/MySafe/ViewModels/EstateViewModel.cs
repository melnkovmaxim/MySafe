using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.ViewModels.Abstractions;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class EstateViewModel : AuthorizedViewModelBase
    {
        public EstateViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
