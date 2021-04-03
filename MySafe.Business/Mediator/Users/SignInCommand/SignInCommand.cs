using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SignInCommand
{
    /// <summary>
    ///     Вход
    /// </summary>
    public class SignInCommand : RequestBase<Core.Models.Responses.User>
    {
        public SignInCommand(string login, string password)
        {
            User = new User(login, password);
        }

        [JsonProperty("user")] public User User { get; }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "users/sign_in";
    }

    public class User
    {
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public User(string login, string password, string passwordConfirmation, string email, string phoneNumber,
            bool isAgree)
        {
            Login = login;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            Email = email;
            PhoneNumber = phoneNumber;
            IsAgree = isAgree;
        }

        public User()
        {
        }

        [JsonProperty("login")] public string Login { get; set; }

        [JsonProperty("password")] public string Password { get; set; }

        [JsonProperty("password_confirmation")]
        public string PasswordConfirmation { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }

        [JsonProperty("user_agreement")] public bool IsAgree { get; set; }
    }
}