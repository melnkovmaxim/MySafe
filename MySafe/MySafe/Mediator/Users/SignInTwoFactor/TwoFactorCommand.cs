using MediatR;
using MySafe.Presentation.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Users.SignInTwoFactor
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
