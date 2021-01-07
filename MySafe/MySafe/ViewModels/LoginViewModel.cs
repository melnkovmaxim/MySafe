using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MySafe.Models;
using MySafe.Services;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MySafe.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public AuthPassword Password { get; set; }

        private string _secret => "12225";
        private bool _isCorretPassword => _secret == Password.GetPassword();

        public bool IsSet(int k) => true;

        private readonly YandexAuthService _authService;
        private bool _isAuthorized;

        public LoginViewModel(INavigationService navigationService, YandexAuthService authService)
            : base(navigationService)
        {
            Title = "Авторизация";
            _authService = authService;
            Password = new AuthPassword();
        }

        public DelegateCommand FingerPrintCommand => new DelegateCommand(async () =>
        {
            var request = new AuthenticationRequestConfiguration("Вход в MySafe", "Коснитесь сканера отпечатков");
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

        public DelegateCommand<string> EnterNumberCommand => new DelegateCommand<string>((number) =>
        {
            Password.Push(int.Parse(number));
            if (_isCorretPassword)
            {

            }
        });

        public DelegateCommand RemoveLastNumberCommand => new DelegateCommand(() =>
        {
            Password.Pop();
        });

        public DelegateCommand RestorePasswordCommand => new DelegateCommand(() =>
        {
            // App.Current.MainPage.DisplayAlert("Вы забыли пароль!", "Восстанавливаем...", "Отмена");
        });
    }
}
