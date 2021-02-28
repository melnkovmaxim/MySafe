using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Trash.GetTrashInfo
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
