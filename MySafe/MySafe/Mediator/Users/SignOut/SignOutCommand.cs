using MediatR;
using MySafe.Presentation.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Users.SignOut
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
