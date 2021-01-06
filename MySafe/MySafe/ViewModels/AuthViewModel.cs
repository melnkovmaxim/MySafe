using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MySafe.Services;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;

namespace MySafe.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private readonly YandexAuthService _authService;
        private bool _isAuthorized;

        public AuthViewModel(INavigationService navigationService, YandexAuthService authService)
            : base(navigationService)
        {
            Title = "Авторизация";
            _authService = authService;
        }

        private ICommand _auth;
        public ICommand Auth => _auth ??= new DelegateCommand(async () =>
        {
            
            var request = new AuthenticationRequestConfiguration ("Вход в MySafe", "Коснитесь сканера отпечатков");
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            if (result.Authenticated)
            {
                // do secret stuff :)

            }
            else
            {
                // not allowed to do secret stuff :(
            }
        });
    }
}
