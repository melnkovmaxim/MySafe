using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Data.Abstractions
{
    public interface ISecureStorageRepository
    {
        Task<string> GetDevicePasswordAsync();
        Task SetDevicePasswordAsync(string password);
        Task RemoveDevicePasswordAsync();

        Task<string> GetJwtTokenForTwoFactorAsync();
        Task SetJwtTokenForTwoFactorAsync(string token);
        
        Task<JwtSecurityToken> GetJwtSecurityTokenAsync();
        Task<string> GetJwtTokenAsync();
        Task SetJwtTokenAsync(string jwtToken);
        Task RemoveJwtToken();
    }
}
