using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Business.Mediator.Trash.ClearTrash
{
    public class ClearTrashCommand: IRequest<BaseResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }

        public ClearTrashCommand(JwtSecurityToken jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
