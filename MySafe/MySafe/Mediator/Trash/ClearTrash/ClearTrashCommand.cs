using MediatR;
using MySafe.Presentation.Models.Responses.Abstractions;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Trash.ClearTrash
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
