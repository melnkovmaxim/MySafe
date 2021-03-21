using Fody;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using MySafe.Data.Abstractions;
using Xamarin.Essentials;

namespace MySafe.Presentation.Repositories
{
    [ConfigureAwait(false)]
    public class SecureStorageRepository : ISecureStorageRepository
    {
        private const string TWO_FACTOR_JWT_TOKEN_KEY = "TwoFactorToken";
        private const string MAIN_JWT_TOKEN_KEY = "MainToken";
        private const string DEVICE_PASSWORD_KEY = "DevicePassword";
        private const string USER_LOGIN = "UserLogin";

        public Task<string> GetDevicePasswordAsync()
        {
            return SecureStorage.GetAsync(DEVICE_PASSWORD_KEY);
        }

        public Task SetDevicePasswordAsync(string password)
        {
            return SecureStorage.SetAsync(DEVICE_PASSWORD_KEY, password);
        }

        public Task RemoveDevicePasswordAsync()
        {
            return SetDevicePasswordAsync(string.Empty);
        }

        public Task<string> GetJwtTokenAsync()
        {
            return SecureStorage.GetAsync(MAIN_JWT_TOKEN_KEY);
        }

        public Task<string> GetJwtTokenForTwoFactorAsync()
        {
            return SecureStorage.GetAsync(TWO_FACTOR_JWT_TOKEN_KEY);
        }
        public Task SetJwtTokenForTwoFactorAsync(string token)
        {
            return SecureStorage.SetAsync(TWO_FACTOR_JWT_TOKEN_KEY, token);
        }

        public async Task<JwtSecurityToken> GetJwtSecurityTokenAsync()
        {
            var token = await SecureStorage.GetAsync(MAIN_JWT_TOKEN_KEY);

            return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token); 
        }
        public async Task<JwtSecurityToken> GetJwtSecurityTokenTwoFactorAsync()
        {
            var token = await SecureStorage.GetAsync(TWO_FACTOR_JWT_TOKEN_KEY);

            return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token); 
        }

        public Task SetJwtTokenAsync(string jwtToken)
        {
            return SecureStorage.SetAsync(MAIN_JWT_TOKEN_KEY, jwtToken);
        }

        public Task RemoveJwtToken()
        {
            return SecureStorage.SetAsync(MAIN_JWT_TOKEN_KEY, string.Empty);
        }

        public Task RemoveTwoFactorJwtToken()
        {
            return SecureStorage.SetAsync(TWO_FACTOR_JWT_TOKEN_KEY, string.Empty);
        }

        public Task SetUserLogin(string login)
        {
            return SecureStorage.SetAsync(USER_LOGIN, login);
        }
        public Task<string> GetUserLogin()
        {
            return SecureStorage.GetAsync(USER_LOGIN);
        }
    }
}
