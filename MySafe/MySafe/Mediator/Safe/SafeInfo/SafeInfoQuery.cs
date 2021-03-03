using MediatR;
using MySafe.Presentation.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Safe.SafeInfo
{
    public class SafeInfoQuery : IRequest<SafeResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }

        public SafeInfoQuery(JwtSecurityToken jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
