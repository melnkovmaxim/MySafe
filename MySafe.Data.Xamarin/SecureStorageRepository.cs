using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Fody;
using Microsoft.AppCenter;
using MySafe.Domain.Repositories;
using Xamarin.Essentials;

namespace MySafe.Data.Xamarin
{
    [ConfigureAwait(false)]
    public class SecureStorageRepository : ISecureStorageRepository
    {
        private const string TWO_FACTOR_JWT_TOKEN_KEY = "TwoFactorToken";
        private const string MAIN_JWT_TOKEN_KEY = "MainToken";
        private const string DEVICE_PASSWORD_KEY = "DevicePassword";
        private const string USER_LOGIN = "UserLogin";
        private const string REFRESH_JWT_TOKEN_KEY = "RefreshToken";

        private static string TwoFactorJwtToken = "";
        private static string MainJwtToken = "";
        private static string DevicePassword = "";
        private static string UserLogin = "";
        private static string RefreshJwtToken = "";


        public Task<string> GetDevicePasswordAsync()
        {
            return Task.FromResult(DevicePassword);
            //return SecureStorage.GetAsync(DEVICE_PASSWORD_KEY);
        }

        public Task SetDevicePasswordAsync(string password)
        {
            DevicePassword = password;
            return Task.CompletedTask;
            //return SecureStorage.SetAsync(DEVICE_PASSWORD_KEY, password);
        }

        public Task RemoveDevicePasswordAsync()
        {
            DevicePassword = string.Empty;
            return Task.CompletedTask;
            //return SetDevicePasswordAsync(string.Empty);
        }

        public Task<string> GetJwtTokenAsync()
        {
            return Task.FromResult(MainJwtToken);
            //return SecureStorage.GetAsync(MAIN_JWT_TOKEN_KEY);
        }

        public Task<string> GetJwtTokenForTwoFactorAsync()
        {
            return Task.FromResult(TwoFactorJwtToken);
            //return SecureStorage.GetAsync(TWO_FACTOR_JWT_TOKEN_KEY);
        }

        public Task SetJwtTokenForTwoFactorAsync(string token)
        {
            TwoFactorJwtToken = token;
            return Task.CompletedTask;
            //return SecureStorage.SetAsync(TWO_FACTOR_JWT_TOKEN_KEY, token);
        }

        public Task<JwtSecurityToken> GetJwtSecurityTokenAsync()
        {
            return Task.FromResult(string.IsNullOrEmpty(MainJwtToken) ? null : new JwtSecurityToken(MainJwtToken));

            //var token = await SecureStorage.GetAsync(MAIN_JWT_TOKEN_KEY);

            //return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token);
        }

        public Task<JwtSecurityToken> GetJwtSecurityTokenTwoFactorAsync()
        {
            return Task.FromResult(string.IsNullOrEmpty(TwoFactorJwtToken) ? null : new JwtSecurityToken(TwoFactorJwtToken));
            //var token = await SecureStorage.GetAsync(TWO_FACTOR_JWT_TOKEN_KEY);

            //return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token);
        }

        public Task SetJwtTokenAsync(string jwtToken)
        {
            MainJwtToken = jwtToken;
            return Task.CompletedTask;
            //return SecureStorage.SetAsync(MAIN_JWT_TOKEN_KEY, jwtToken);
        }

        public Task RemoveJwtToken()
        {
            MainJwtToken = string.Empty;
            return Task.CompletedTask;
            //return SecureStorage.SetAsync(MAIN_JWT_TOKEN_KEY, string.Empty);
        }

        public Task RemoveTwoFactorJwtToken()
        {
            TwoFactorJwtToken = string.Empty;
            return Task.CompletedTask;
            //return SecureStorage.SetAsync(TWO_FACTOR_JWT_TOKEN_KEY, string.Empty);
        }

        public Task SetRefreshTokenAsync(string refreshToken)
        {
            RefreshJwtToken = refreshToken;
            return Task.CompletedTask;
            //return SecureStorage.SetAsync(REFRESH_JWT_TOKEN_KEY, refreshToken);
        }

        public Task<string> GetRefreshJwtAsync()
        {
            return Task.FromResult(RefreshJwtToken);
            //return SecureStorage.GetAsync(REFRESH_JWT_TOKEN_KEY);
        }
        public Task<JwtSecurityToken> GetRefreshTokenAsync()
        {
            return Task.FromResult(string.IsNullOrEmpty(RefreshJwtToken) ? null : new JwtSecurityToken(RefreshJwtToken));
            //var token = await SecureStorage.GetAsync(REFRESH_JWT_TOKEN_KEY);

            //return string.IsNullOrEmpty(token) ? null : new JwtSecurityToken(token);
        }

        public Task RemoveRefreshTokenAsync()
        {
            RefreshJwtToken = string.Empty;
            return Task.CompletedTask;
            //return SecureStorage.SetAsync(REFRESH_JWT_TOKEN_KEY, string.Empty);
        }

        public Task SetUserLogin(string login)
        {
            UserLogin = login;
            return Task.CompletedTask;
            //return SecureStorage.SetAsync(USER_LOGIN, login);
        }

        public Task<string> GetUserLogin()
        {
            return Task.FromResult(UserLogin);
            //return SecureStorage.GetAsync(USER_LOGIN);
        }
    }
}