using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.RefreshTokenQuery
{
    public class RefreshTokenQuery: BearerRequestBase<UserEntity>
    {
        public readonly string RefreshToken;

        public RefreshTokenQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "auth/jwt-refresh";
    }
}
