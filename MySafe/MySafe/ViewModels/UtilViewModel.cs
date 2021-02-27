using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.ViewModels.Abstractions;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class UtilViewModel : AuthorizedViewModelBase
    {
        public UtilViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        protected override void ActionAfterLoadPage()
        {
            throw new NotImplementedException();
        }
    }
}
