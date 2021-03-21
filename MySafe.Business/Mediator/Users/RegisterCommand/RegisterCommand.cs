using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Business.Mediator.Users.SignInCommand;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Users.RegisterCommand
{
    /// <summary>
    /// Регистрация
    /// </summary>
    public class RegisterCommand : RequestBase<MySafe.Core.Entities.Responses.User>
    {
        [JsonProperty("user")]
        public User User { get; }

        public RegisterCommand(User user)
        {
            User = user;
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "/users";
    }
}
