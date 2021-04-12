using MySafe.Core;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.RegisterCommand
{
    /// <summary>
    ///     Регистрация
    /// </summary>
    public class RegisterCommand : RequestBase<UserEntity>
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string PhoneNumber { get; set; }
        public bool UserAgreement { get; set; }

        public RegisterCommand(string email, string login, string phoneNumber, string password,
            string passwordConfirmation, bool userAgreement)
        {
            Email = email;
            Login = login;
            PhoneNumber = phoneNumber;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            UserAgreement = userAgreement;
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "/auth/create-account";
    }
}