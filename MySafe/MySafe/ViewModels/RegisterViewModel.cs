using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Business.Mediator.Users.RegisterCommand;
using MySafe.Business.Mediator.Users.SignInCommand;
using MySafe.Core.Commands;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class RegisterViewModel: ViewModelBase
    {
        private readonly IMediator _mediator;
        public AsyncCommand RegisterCommand { get; set; }
        public User User { get; set; }

        public RegisterViewModel(INavigationService navigationService, IMediator mediator) : base(navigationService)
        {
            _mediator = mediator;

            User = new User() { Email = "Ваша почта", IsAgree = true, Login = "Ваш логин", Password = "123456", PasswordConfirmation = "123456", PhoneNumber = "81234567890"};
            RegisterCommand = new AsyncCommand(RegisterCommandTask);
        }

        private async Task RegisterCommandTask()
        {
            var result = await _mediator.Send(new RegisterCommand(User));

            if (result.HasError)
            {
                Error = result.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(SignInPage));
        }
    }
}
