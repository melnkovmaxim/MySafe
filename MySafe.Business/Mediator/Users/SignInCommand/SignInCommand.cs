using MediatR;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Users.SignInCommand
{
    /// <summary>
    /// Вход
    /// </summary>
    public class SignInCommand : IRequest<User>
    {
        public string Login { get; }
        public string Password { get; }

        public SignInCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
