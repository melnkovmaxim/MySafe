using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Safe.SafeInfoQuery
{
    /// <summary>
    /// Получить информацию о сейфе
    /// </summary>
    public class SafeInfoQuery : IRequest<Core.Entities.Responses.Safe>
    {
        public JwtSecurityToken JwtToken { get; set; }

        public SafeInfoQuery(JwtSecurityToken jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
