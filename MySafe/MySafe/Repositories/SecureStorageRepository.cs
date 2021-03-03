﻿using Fody;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using MySafe.Data.Abstractions;
using Xamarin.Essentials;

namespace MySafe.Presentation.Repositories
{
    [ConfigureAwait(false)]
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

        public async Task<JwtSecurityToken> GetJstTokenAsync()
        {
            var token = await SecureStorage.GetAsync(nameof(JwtSecurityToken))
                .ConfigureAwait(false);

            return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token); 
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
