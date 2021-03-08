using MediatR;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Trash.TrashContentQuery
{
    /// <summary>
    /// Получить содержимое корзины
    /// </summary>
    public class TrashContentQuery: IRequest<List<TrashResponse>>
    {
        public JwtSecurityToken JwtToken { get; set; }

        public TrashContentQuery(JwtSecurityToken jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
