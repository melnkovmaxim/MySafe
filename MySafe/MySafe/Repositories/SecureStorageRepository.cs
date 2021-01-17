using System;
using MySafe.Repositories.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MySafe.Repositories
{
    public class SecureStorageRepository : ISecureStorageRepository
    {
        public Task<string> GetLocalPasswordAsync()
        {
            return SecureStorage.GetAsync(App.Resources.PasswordPath);
        }

        public Task SetLocalPasswordAsync(string password)
        {
            return SecureStorage.SetAsync(App.Resources.PasswordPath, password);
        }

        public Task RemovePasswordAsync()
        {
            return SetLocalPasswordAsync(string.Empty);
        }
    }
}
