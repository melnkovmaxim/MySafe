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
        private readonly IMediator _mediator;
        private AsyncCommand _signInCommand;

        private int _remainingLifeTime;
        public string RemainingLifeTimeMessage { get; set; }

        public string Code { get; set; }

        public TwoFactorViewModel(INavigationService navigationService, IMediator mediator)
            :base(navigationService)
        {
            _mediator = mediator;
            _remainingLifeTime = 60;
            Device.StartTimer(TimeSpan.FromSeconds(1), TimerCallback);
        }
        
        private bool TimerCallback()
        {
            if (_remainingLifeTime > 0)
            {
                RemainingLifeTimeMessage = $"Осталось {--_remainingLifeTime}";
                return true;
            }

            Device.BeginInvokeOnMainThread(async () => await _navigationService.NavigateAsync(nameof(SignInPage)));
            return false;
        }


        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            var response = await _mediator.Send(new TwoFactorAuthenticationCommand(Code));

            if (response.HasError)
            {
                Error = response.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(MainPage));
        });
    }
}
