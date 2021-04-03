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
        public readonly string Email;
        public readonly string Login;
        public readonly string Password;
        public readonly string PasswordConfirmation;
        public readonly string PhoneNumber;
        public readonly bool UserAgreement;

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
        public override string RequestResource => "/users";
    }
}