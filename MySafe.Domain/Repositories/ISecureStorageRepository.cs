using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace MySafe.Domain.Repositories
{
    public interface ISecureStorageRepository
    {
        Task<string> GetUserLogin();
        Task SetUserLogin(string login);

        Task<string> GetDevicePasswordAsync();
        Task SetDevicePasswordAsync(string password);
        Task RemoveDevicePasswordAsync();


        Task<JwtSecurityToken> GetAccessSecurityTokenAsync();
        Task<string> GetAccessJwtAsync();
        Task SetAccessJwtAsync(string jwtToken);
        Task RemoveAccessJwtAsync();
        
        Task SetTwoFactorJwtAsync(string token);
        Task<JwtSecurityToken> GetTwoFactorSecurityTokenAsync();
        Task RemoveTwoFactorJwtAsync();
        Task<string> GetTwoFactorJwtAsync();

        Task SetRefreshTokenAsync(string refreshToken);
        Task<string> GetRefreshTokenAsync();
        Task RemoveRefreshTokenAsync();
    }
}