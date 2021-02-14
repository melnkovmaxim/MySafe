using System;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Repositories.Abstractions;
using System.Threading.Tasks;
using Fody;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace MySafe.Repositories
{
    [ConfigureAwait(false)]
    public class SecureStorageRepository : ISecureStorageRepository
    {
        private readonly ISecureStorage _secureStorage;

        public SecureStorageRepository(ISecureStorage secureStorage)
        {
            _secureStorage = secureStorage;
        }

        public Task<string> GetLocalPasswordAsync()
        {
            return _secureStorage.GetAsync(App.Resources.PasswordPath);
        }

        public Task SetLocalPasswordAsync(string password)
        {
            return _secureStorage.SetAsync(App.Resources.PasswordPath, password);
        }

        public Task RemovePasswordAsync()
        {
            return SetLocalPasswordAsync(string.Empty);
        }

        public Task<string> GetTokenAsync()
        {
            return _secureStorage.GetAsync(nameof(JwtSecurityToken));
        }

        public async Task<JwtSecurityToken> GetJstTokenAsync()
        {
            var token = await _secureStorage.GetAsync(nameof(JwtSecurityToken))
                .ConfigureAwait(false);

            return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token); 
        }

        public Task SetTokenAsync(string jwtToken)
        {
            return _secureStorage.SetAsync(nameof(JwtSecurityToken), jwtToken);
        }

        public Task RemoveToken()
        {
            return _secureStorage.SetAsync(nameof(JwtSecurityToken), string.Empty);
        }
    }
}
