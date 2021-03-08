using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand
{
    /// <summary>
    /// Вход. Второй фактор
    /// </summary>
    public class TwoFactorAuthenticationCommand : IRequest<User>
    {
        public string Code { get; set; }
        public JwtSecurityToken JwtToken { get; set; }

        public TwoFactorAuthenticationCommand(string code, JwtSecurityToken jwtToken)
        {
            Code = code;
            JwtToken = jwtToken;
        }
    }
}
