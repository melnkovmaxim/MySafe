using System;
using System.Threading.Tasks;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.Presentation.ViewModels
{
    public class TwoFactorViewModel : ViewModelBase
    {
        private const int VIEW_LIFETIME = 180;
        private readonly DateTime _exitTime;
        private readonly IMediator _mediator;
        private readonly TimeSpan TIMER_INTERVAL_IN_SECONDS = TimeSpan.FromSeconds(1);

        public TwoFactorViewModel(INavigationService navigationService, IMediator mediator)
            : base(navigationService)
        {
            _mediator = mediator;
            _exitTime = DateTime.Now.AddSeconds(VIEW_LIFETIME);

            SignInCommand = new AsyncCommand(SignInCommandTask);

            Device.StartTimer(TIMER_INTERVAL_IN_SECONDS, TimerCallback);
        }

        public AsyncCommand SignInCommand { get; }
        public string RemainingLifeTimeMessage { get; set; }
        public string Code { get; set; }

        private bool TimerCallback()
        {
            if (DateTime.Now > _exitTime)
            {
                Device.BeginInvokeOnMainThread(async () => await _navigationService.NavigateAsync(nameof(SignInPage)));
                return false;
            }

            var remainitTime = _exitTime - DateTime.Now;
            RemainingLifeTimeMessage = $"Осталось {remainitTime.Minutes}:{remainitTime.Seconds}";

            return true;
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
    }
}