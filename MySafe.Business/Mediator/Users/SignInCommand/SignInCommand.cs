using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SignInCommand
{
    /// <summary>
    ///     Вход
    /// </summary>
    public class SignInCommand : RequestBase<UserEntity>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public SignInCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "users/sign_in";
    }
}