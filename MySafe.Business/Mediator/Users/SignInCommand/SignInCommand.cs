using MediatR;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Users.SignInCommand
{
    /// <summary>
    /// Вход
    /// </summary>
    public class SignInCommand : RequestBase<MySafe.Core.Entities.Responses.User>
    {
        [JsonProperty("user")]
        public User User { get; }

        public SignInCommand(string login, string password)
        {
            User = new User(login, password);
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "users/sign_in";

    }
    public class User
    {
        [JsonProperty("login")]
        public string Login { get; }

        [JsonProperty("password")]
        public string Password { get; }

        [JsonProperty("password_confirmation")]
        public string PasswordConfirmation { get; }

        [JsonProperty("email")]
        public string Email { get; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; }

        [JsonProperty("user_agreement")]
        public bool IsAgree { get; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public User(string login, string password, string passwordConfirmation, string email, string phoneNumber, bool isAgree)
        {
            Login = login;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            Email = email;
            PhoneNumber = phoneNumber;
            IsAgree = isAgree;
        }
    }
}
