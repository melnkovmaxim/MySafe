using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace MySafe.Mediator.Users.SignIn
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {
            RuleFor(s => s.Login)
                .NotEmpty()
                .WithMessage("Введите логин");

            RuleFor(s => s.Password)
                .NotEmpty()
                .WithMessage("Введите пароль");
        }
    }
}
