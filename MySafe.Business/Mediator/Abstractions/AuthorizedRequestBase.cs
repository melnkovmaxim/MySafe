using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Abstractions
{
    public abstract class AuthorizedRequestBase<T>: IRequest<T>
    {
        public string JwtToken { get; set; }

        public AuthorizedRequestBase(string jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
