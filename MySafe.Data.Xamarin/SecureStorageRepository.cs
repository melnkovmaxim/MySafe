using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Fody;
using MySafe.Core.Interfaces.Repositories;
using Xamarin.Essentials;

namespace MySafe.Data.Xamarin
{
    [ConfigureAwait(false)]
    public class SecureStorageRepository : ISecureStorageRepository
    {
        private const string TWO_FACTOR_JWT_KEY = "TwoFactorToken";
        private const string ACCESS_JWT_KEY = "MainToken";
        private const string DEVICE_PASSWORD_KEY = "DevicePassword";
        private const string USER_LOGIN = "UserLogin";
        private const string REFRESH_JWT_KEY = "RefreshToken";
        
        public Task SetDevicePasswordAsync(string password)
        {
            return SecureStorage.SetAsync(DEVICE_PASSWORD_KEY, password ?? string.Empty);
        }

        public Task<string> GetDevicePasswordAsync()
        {
            return SecureStorage.GetAsync(DEVICE_PASSWORD_KEY);
        }

        public Task RemoveDevicePasswordAsync()
        {
            return SetDevicePasswordAsync(string.Empty);
        }

        public Task SetAccessJwtAsync(string jwtToken)
        {
            return SecureStorage.SetAsync(ACCESS_JWT_KEY, jwtToken ?? string.Empty);
        }

        public Task<string> GetAccessJwtAsync()
        {
            return SecureStorage.GetAsync(ACCESS_JWT_KEY);
        }

        public async Task<JwtSecurityToken> GetAccessSecurityTokenAsync()
        {
            var token = await SecureStorage.GetAsync(ACCESS_JWT_KEY);

            return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token);
        }

        public Task RemoveAccessJwtAsync()
        {
            return SecureStorage.SetAsync(ACCESS_JWT_KEY, string.Empty);
        }

        public Task SetTwoFactorJwtAsync(string token)
        {
            return SecureStorage.SetAsync(TWO_FACTOR_JWT_KEY, token ?? string.Empty);
        }

        public Task<string> GetTwoFactorJwtAsync()
        {
            return SecureStorage.GetAsync(TWO_FACTOR_JWT_KEY);
        }

        public async Task<JwtSecurityToken> GetTwoFactorSecurityTokenAsync()
        {
            var token = await SecureStorage.GetAsync(TWO_FACTOR_JWT_KEY);

            return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token);
        }

        public Task RemoveTwoFactorJwtAsync()
        {
            return SecureStorage.SetAsync(TWO_FACTOR_JWT_KEY, string.Empty);
        }

        public Task SetRefreshTokenAsync(string refreshToken)
        {
            return SecureStorage.SetAsync(REFRESH_JWT_KEY, refreshToken ?? string.Empty);
        }

        public Task<string> GetRefreshTokenAsync()
        {
            return SecureStorage.GetAsync(REFRESH_JWT_KEY);
        }

        public Task RemoveRefreshTokenAsync()
        {
            return SecureStorage.SetAsync(REFRESH_JWT_KEY, string.Empty);
        }

        public Task SetUserLogin(string login)
        {
            return SecureStorage.SetAsync(USER_LOGIN, login ?? string.Empty);
        }

        public Task<string> GetUserLogin()
        {
            return SecureStorage.GetAsync(USER_LOGIN);
        }
    }
}