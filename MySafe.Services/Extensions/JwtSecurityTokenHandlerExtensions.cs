using System;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Services.Extensions
{
    public static class JwtSecurityTokenHandlerExtensions
    {
        public static bool IsExpired(this JwtSecurityToken jwtToken)
        {
            if (jwtToken.ValidTo.ToUniversalTime() < DateTime.UtcNow.AddMinutes(5)) return true;

            return false;
        }
    }
}