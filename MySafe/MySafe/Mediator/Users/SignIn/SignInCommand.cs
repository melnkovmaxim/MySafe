using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Users.SignIn
{
    public class SignInCommand : IRequest<UserResponse>
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
