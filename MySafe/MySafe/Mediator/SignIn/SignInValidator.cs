using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MySafe.Mediator.SignIn
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {
            RuleFor(s => s.Login)
                .NotEmpty();

            RuleFor(s => s.Password)
                .NotEmpty();
        }
    }
}
