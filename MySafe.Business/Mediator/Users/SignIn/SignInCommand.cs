using MediatR;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Users.SignIn
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
