using MediatR;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Trash.GetTrashInfo
{
    public class TrashInfoQuery: IRequest<List<TrashResponse>>
    {
        public JwtSecurityToken JwtToken { get; set; }

        public TrashInfoQuery(JwtSecurityToken jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
