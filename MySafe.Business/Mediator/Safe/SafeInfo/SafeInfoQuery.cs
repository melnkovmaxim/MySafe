using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Safe.SafeInfo
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
