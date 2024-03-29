﻿using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Users.RegisterCommand;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AsyncCommand RegisterCommand { get; set; }
        public User User { get; set; }

        public RegisterViewModel(
            INavigationService navigationService,
            IMediator mediator,
            IMapper mapper) : base(navigationService)
        {
            _mediator = mediator;
            _mapper = mapper;

            User = new User
            {
                Email = "Ваша почта", UserAgreement = true, Login = "Ваш логин", Password = "123456",
                PasswordConfirmation = "123456", PhoneNumber = "81234567890"
            };
            RegisterCommand = new AsyncCommand(RegisterCommandTask);
        }

        private async Task RegisterCommandTask()
        {
            var registerCommand = _mapper.Map<RegisterCommand>(User);
            var result = await _mediator.Send(registerCommand);

            if (result.HasError)
            {
                Error = result.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(SignInPage));
        }
    }
}