using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace MySafe.Repositories
{
    /// <summary>
    /// Обёртка над SecureStorage.
    /// Необходимо для создания мока SecureStorage, т.к. класс является статическим.
    /// Поможет при тестировании репозитория
    /// </summary>
    public class SecureStorageWrapper : ISecureStorage
    {
        public Task<string> GetAsync(string key)
        {
            return SecureStorage.GetAsync(key);
        }

        public Task SetAsync(string key, string value)
        {
            return SecureStorage.SetAsync(key, value);
        }

        public bool Remove(string key)
        {
            return SecureStorage.Remove(key);
        }

        public void RemoveAll()
        {
            SecureStorage.RemoveAll();
        }
    }
}
