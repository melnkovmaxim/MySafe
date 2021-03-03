using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Users.SignOut
{
    public class SignOutCommand : IRequest<UserResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }

        public SignOutCommand(JwtSecurityToken jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
