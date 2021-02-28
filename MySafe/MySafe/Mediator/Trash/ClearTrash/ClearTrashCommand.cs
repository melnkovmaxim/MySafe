using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Trash.ClearTrash
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
