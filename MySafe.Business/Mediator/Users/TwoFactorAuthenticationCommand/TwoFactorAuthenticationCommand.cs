using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand
{
    /// <summary>
    ///     Вход. Второй фактор
    /// </summary>
    public class TwoFactorAuthenticationCommand : BearerRequestBase<UserEntity>
    {
        public TwoFactorAuthenticationCommand(string code)
        {
            Code = code;
        }

        public string Code { get; set; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => "users/two_factor_authentication";
    }
}