using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Safe.SafeInfo
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
