using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MySafe.Mediator.SignIn
{
    public class SignInCommand : IRequest<string>
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
