using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand
{
    /// <summary>
    /// Вход. Второй фактор
    /// </summary>
    public class TwoFactorAuthenticationCommand : BearerRequestBase<User>
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        public TwoFactorAuthenticationCommand(string code)
        {
            Code = code;
        }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => "users/two_factor_authentication";
    }
}
