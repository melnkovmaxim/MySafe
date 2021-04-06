using FluentValidation;

namespace MySafe.Services.Mediator.Users.SignInCommand
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