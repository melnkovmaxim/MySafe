using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Mediator.SignIn
{
    public class SignInCommand : IRequest<JwtSecurityToken>
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
