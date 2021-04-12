using System;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Services.Extensions
{
    public static class JwtSecurityTokenHandlerExtensions
    {
        public static bool IsExpired(this JwtSecurityToken jwtToken)
        {
            if (jwtToken == null || DateTime.UtcNow.AddMinutes(5) > jwtToken.ValidTo.ToUniversalTime()) return true;

            return false;
        }
    }
}