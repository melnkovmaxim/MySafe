using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.MediatorResponses;
using RestSharp;

namespace MySafe.Mediator.SignInTwoFactor
{
    public class TwoFactorCommand : IRequest<UserResponse>
    {
        public string Code { get; set; }
        public JwtSecurityToken JwtToken { get; set; }

        public TwoFactorCommand(string code, JwtSecurityToken jwtToken)
        {
            Code = code;
            JwtToken = jwtToken;
        }
    }
}
