using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace MySafe.Presentation.Models
{
    public class NavigationParameter
    {
        public readonly int ChildId;
        public readonly string ChildName;

        public NavigationParameter(int childId, string childName)
        {
            ChildId = childId;
            ChildName = childName;
        }
    }
}
