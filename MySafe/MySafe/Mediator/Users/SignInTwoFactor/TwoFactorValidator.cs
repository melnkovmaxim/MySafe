using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MySafe.Mediator.Users.SignInTwoFactor
{
    public class TwoFactorValidator : AbstractValidator<TwoFactorCommand>
    {
        public TwoFactorValidator()
        {
            RuleFor(s => s.Code)
                .NotEmpty()
                .WithMessage("Введите код из смс");

            RuleFor(s => s.JwtToken)
                .NotEmpty()
                .WithMessage("Что-то пошло не так...");
        }
    }
}
