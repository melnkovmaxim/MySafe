using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Business.Mediator.Trash.ClearTrashCommand
{
    /// <summary>
    /// Очистить корзину
    /// </summary>
    public class ClearTrashCommand: IRequest<ResponseBase>
    {
        public JwtSecurityToken JwtToken { get; set; }

        public ClearTrashCommand(JwtSecurityToken jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
