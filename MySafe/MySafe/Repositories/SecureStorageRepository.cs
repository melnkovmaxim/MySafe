using System;
using System.IdentityModel.Tokens.Jwt;
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

        public Task<string> GetTokenAsync()
        {
            return SecureStorage.GetAsync(nameof(JwtSecurityToken));
        }

        public Task SetTokenAsync(string jwtToken)
        {
            return SecureStorage.SetAsync(nameof(JwtSecurityToken), jwtToken);
        }

        public Task RemoveToken()
        {
            return SecureStorage.SetAsync(nameof(JwtSecurityToken), string.Empty);
        }
    }
}
