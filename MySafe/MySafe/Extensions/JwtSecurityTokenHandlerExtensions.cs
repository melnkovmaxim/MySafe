using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MySafe.Extensions
{
    public static class JwtSecurityTokenHandlerExtensions
    {
        public static JwtSecurityToken GetJwtTokenFromResponse(this JwtSecurityTokenHandler handler, IRestResponse response)
        {
            var authorizationToken = response.Headers
                .FirstOrDefault(x => x.Name == "Authorization")?.Value
                .ToString()
                .Replace("Bearer ", string.Empty);
            
            var jwtToken = handler.ReadJwtToken(authorizationToken);

            return jwtToken;
        }
    }
}
