using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Users.SignInTwoFactor
{
    public class TwoFactorCommand : IRequest<UserResponse>
    {
        public string Code { get; set; }
        public JwtSecurityToken JwtToken { get; set; }

        public TwoFactorCommand(string code, JwtSecurityToken jwtToken)
        {
            Code = code;
            JwtToken = jwtToken;
        }
    }
}
