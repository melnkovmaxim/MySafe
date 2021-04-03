using System.Collections.ObjectModel;
using System.Linq;

namespace MySafe.Core.Models
{
    public class ApplicationPassword
    {
        public ApplicationPassword()
        {
            PasswordCollection = new ObservableCollection<string>();
        }

        public ObservableCollection<string> PasswordCollection { get; }
        public string Password => string.Join("", PasswordCollection.Reverse());
        public int PasswordMaxLength => MySafeApp.Resources.DefaultApplicationPasswordLength;
        public int PasswordLength => PasswordCollection.Count(x => !string.IsNullOrEmpty(x));
    }
}