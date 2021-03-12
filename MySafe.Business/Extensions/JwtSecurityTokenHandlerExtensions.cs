using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core.Entities.Responses.Abstractions;
using RestSharp;

namespace MySafe.Business.Extensions
{
    public static class JwtSecurityTokenHandlerExtensions
    {
        public static bool IsValidToken(this JwtSecurityToken jwtToken)
        {
            if (jwtToken?.ValidTo.ToUniversalTime() > DateTime.UtcNow.AddMinutes(5))
            {
                return true;
            }

            return false;
        }
    }
}
