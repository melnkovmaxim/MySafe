﻿using MediatR;
using MySafe.Models.MediatorResponses;

namespace MySafe.Mediator.SignIn
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
