using MediatR;
using MySafe.Presentation.Models.Responses;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Trash.GetTrashInfo
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
