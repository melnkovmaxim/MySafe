using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Repositories.Abstractions
{
    public interface ISecureStorageRepository
    {
        Task<string> GetLocalPasswordAsync();
        Task SetLocalPasswordAsync(string password);
        Task RemovePasswordAsync();
        Task<string> GetTokenAsync();
        Task<JwtSecurityToken> GetJstTokenAsync();
        Task SetTokenAsync(string jwtToken);
        Task RemoveToken();
    }
}
