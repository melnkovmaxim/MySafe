using System.Collections.ObjectModel;
using System.Linq;
using MySafe.Core;
using MySafe.Core.Commands;
using MySafe.Domain.Services;

namespace MySafe.Services.Services
{
    public class PasswordManager : BindableBase, IPasswordManagerService, ITransientService
    {
        public PasswordManager()
        {
            PasswordCollection = new ObservableCollection<string>();
            SetPasswordLength(PasswordMaxLength);
        }

        public ObservableCollection<string> PasswordCollection { get; }
        public string Password => string.Join("", PasswordCollection.Reverse());
        public int PasswordMaxLength => MySafeApp.Resources.DefaultApplicationPasswordLength;
        public int PasswordLength => PasswordCollection.Count(x => !string.IsNullOrEmpty(x));

        public void SetPasswordLength(int length)
        {
            PasswordCollection.Clear();

            for (var i = 0; i < length; i++) PasswordCollection.Add(string.Empty);
        }

        public bool TryAdd(string value)
        {
            if (PasswordLength == PasswordMaxLength)
                return false;

            var lastIndex = PasswordCollection.IndexOf(string.Empty);
            PasswordCollection[lastIndex] = value;

            return true;
        }

        public void RemoveLast()
        {
            var @string = PasswordCollection.LastOrDefault(x => !string.IsNullOrEmpty(x));

            if (@string != null)
            {
                var lastIndex = PasswordCollection.ToList().LastIndexOf(@string);
                PasswordCollection[lastIndex] = string.Empty;
            }
        }

        public void Clear()
        {
            for (var i = 0; i < PasswordMaxLength; i++) PasswordCollection[i] = string.Empty;
        }
    }
}