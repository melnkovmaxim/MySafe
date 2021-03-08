using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Core.Entities
{
    public class ApplicationPassword
    {
        public ObservableCollection<string> PasswordCollection { get; }
        public string Password => string.Join("", PasswordCollection.Reverse());
        public int PasswordMaxLength => MySafeApp.Resources.DefaultApplicationPasswordLength;
        public int PasswordLength => PasswordCollection.Count(x => !string.IsNullOrEmpty(x));

        public ApplicationPassword()
        {
            PasswordCollection = new ObservableCollection<string>();
        }
    }
}
