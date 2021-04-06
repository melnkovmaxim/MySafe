using FluentValidation;

namespace MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand
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