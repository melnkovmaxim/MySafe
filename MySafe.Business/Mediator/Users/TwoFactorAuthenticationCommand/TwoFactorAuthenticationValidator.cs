using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand
{
    public class TwoFactorAuthenticationValidator : AbstractValidator<TwoFactorAuthenticationCommand>
    {
        public TwoFactorAuthenticationValidator()
        {
            RuleFor(s => s.Code)
                .NotEmpty()
                .WithMessage("Введите код из смс");

            RuleFor(s => s.JwtToken)
                .NotEmpty()
                .WithMessage("Токен не найден");
        }
    }
}
