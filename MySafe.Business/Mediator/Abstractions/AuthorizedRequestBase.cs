using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Abstractions
{
    public abstract class AuthorizedRequestBase<T>: IRequest<T>
    {
        [JsonIgnore]
        public string JwtToken { get; set; }
        [JsonIgnore]
        public abstract Method RequestMethod { get; }
        [JsonIgnore]
        public abstract string RequestResource { get; }

        protected AuthorizedRequestBase(string jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
