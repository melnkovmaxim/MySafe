using System;
using System.Threading.Tasks;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Users.SmsRequestCommand;
using MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.Presentation.ViewModels
{
    public class TwoFactorViewModel : ViewModelBase
    {
        private const int VIEW_LIFETIME_IN_SECONDS = 180;
        private readonly IMediator _mediator;

        public LifeTime LifeTime { get; }
        public string Code { get; set; }

        public TwoFactorViewModel(INavigationService navigationService, IMediator mediator)
            : base(navigationService)
        {
            _mediator = mediator;
            LifeTime = new LifeTime(TimeSpan.FromSeconds(VIEW_LIFETIME_IN_SECONDS));

            SignInCommand = new AsyncCommand(SignInCommandTask);
            ResendSmsCommand = new AsyncCommand(RsendSmsCommandTask);

            Device.StartTimer(TimeSpan.FromSeconds(1), TimerCallback);
        }

        public AsyncCommand SignInCommand { get; }
        public AsyncCommand ResendSmsCommand { get; }

        private bool TimerCallback()
        {
            LifeTime.UpdateMessage();
            return !LifeTime.IsDead;
        }

        private async Task SignInCommandTask()
        {
            var response = await _mediator.Send(new TwoFactorAuthenticationCommand(Code));

            if (response.HasError)
            {
                Error = response.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(DeviceAuthPage));
        }

        private async Task RsendSmsCommandTask()
        {
            var response = await _mediator.Send(new SmsRequestCommand());

            if (response.HasError)
            {
                Error = response.Error;
                return;
            }

            LifeTime.Restart();
            Device.StartTimer(TimeSpan.FromSeconds(1), TimerCallback);
        }
    }
}