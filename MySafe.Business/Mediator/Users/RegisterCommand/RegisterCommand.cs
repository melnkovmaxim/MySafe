
using MySafe.Services.Mediator.Abstractions;
using MySafe.Services.Mediator.Users.SignInCommand;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Users.RegisterCommand
{
    /// <summary>
    ///     Регистрация
    /// </summary>
    public class RegisterCommand : RequestBase<MySafe.Core.Models.Responses.User>
    {
        public RegisterCommand(User user)
        {
            User = user;
        }

        [JsonProperty("user")] public User User { get; }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "/users";
    }
}