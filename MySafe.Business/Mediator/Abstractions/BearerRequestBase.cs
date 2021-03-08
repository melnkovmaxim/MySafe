using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fody;
using MediatR;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Abstractions
{
    public abstract class BearerRequestBase<T>: RequestBase<T>
    {
        [JsonIgnore]
        public string JwtToken { get; set; }

        protected BearerRequestBase(string jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
