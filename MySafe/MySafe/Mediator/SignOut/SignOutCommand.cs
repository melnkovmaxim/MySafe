﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Extensions;

namespace MySafe.Mediator.SignOut
{
    public class SignOutCommand : IRequest
    {
        public JwtSecurityToken JwtToken { get; set; }

        public SignOutCommand(JwtSecurityToken jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
