using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Models.MediatorResponses
{
    public class UserResponse : BaseResponse
    {
        public JwtSecurityToken JwtToken { get; set; }
    }
}
