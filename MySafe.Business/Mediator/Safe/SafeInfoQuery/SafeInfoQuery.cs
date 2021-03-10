using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Safe.SafeInfoQuery
{
    /// <summary>
    /// Получить информацию о сейфе
    /// </summary>
    public class SafeInfoQuery : BearerRequestBase<Core.Entities.Responses.Safe>
    {
        public override Method RequestMethod => Method.GET;
        public override string RequestResource => "api/v1/my_safe";
    }
}
