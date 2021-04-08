using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace MySafe.Domain.Repositories
{
    public interface ISecureStorageRepository
    {
        Task SetUserLogin(string login);
        Task<string> GetUserLogin();
        Task<string> GetDevicePasswordAsync();
        Task SetDevicePasswordAsync(string password);
        Task RemoveDevicePasswordAsync();

        Task<string> GetJwtTokenForTwoFactorAsync();
        Task SetJwtTokenForTwoFactorAsync(string token);

        Task<JwtSecurityToken> GetJwtSecurityTokenAsync();
        Task<JwtSecurityToken> GetJwtSecurityTokenTwoFactorAsync();
        Task<string> GetJwtTokenAsync();
        Task SetJwtTokenAsync(string jwtToken);
        Task RemoveJwtToken();
        Task RemoveTwoFactorJwtToken();
        Task SetRefreshTokenAsync(string refreshToken);
        Task<string> GetRefreshJwtAsync();
        Task RemoveRefreshTokenAsync();
        Task<JwtSecurityToken> GetRefreshTokenAsync();
    }
}