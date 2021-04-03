using System.Collections.ObjectModel;

namespace MySafe.Domain.Services
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