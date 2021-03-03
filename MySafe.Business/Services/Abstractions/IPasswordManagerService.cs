using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Business.Services.Abstractions
{
    public interface IPasswordManagerService
    {
        ObservableCollection<string> PasswordCollection { get; }
        string Password { get; }
        int PasswordMaxLength { get; }
        int PasswordLength { get; }
        void SetPasswordLength(int length);
        bool TryAdd(string value);
        void RemoveLast();
        void Clear();
    }
}
