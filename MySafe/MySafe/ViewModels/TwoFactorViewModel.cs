using System;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Navigation;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand;
using Prism.AppModel;
using Device = Xamarin.Forms.Device;

namespace MySafe.Presentation.ViewModels
{
    public class TwoFactorViewModel : ViewModelBase
    {
        private const int TIMER_TICKS = 181;
        private const int TIMER_INTERVAL_IN_SECONDS = 1;
        private readonly IMediator _mediator;
        public AsyncCommand SignInCommand { get; }
        public string RemainingLifeTimeMessage { get; set; }
        public string Code { get; set; }
        private int _remainingLifeTime;

        public TwoFactorViewModel(INavigationService navigationService, IMediator mediator)
            :base(navigationService)
        {
            _mediator = mediator;
            _remainingLifeTime = TIMER_TICKS;
            SignInCommand = new AsyncCommand(SignInCommandTask);

            Device.StartTimer(TimeSpan.FromSeconds(TIMER_INTERVAL_IN_SECONDS), TimerCallback);
        }
        
        private bool TimerCallback()
        {
            if (_remainingLifeTime > 0)
            {
                RemainingLifeTimeMessage = $"SMS код отправлен: {--_remainingLifeTime}";
                return true;
            }

            Device.BeginInvokeOnMainThread(async () => await _navigationService.NavigateAsync(nameof(SignInPage)));
            return false;
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
        });
    }
}
