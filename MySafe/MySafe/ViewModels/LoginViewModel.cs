using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MySafe.Models;
using MySafe.Services;
using MySafe.Views;
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

        public LoginViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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

        public DelegateCommand<string> EnterNumberCommand => new DelegateCommand<string>(async (number) =>
        {
            Password.Push(int.Parse(number));
            var password = Password.GetPassword();

            if (await SecureStorage.GetAsync("ApplicationPassword") == password)
            {
                await NavigationService.NavigateAsync(nameof(MainPage));
            }
            else if (password.Length == 5)
            {
                Vibration.Vibrate(TimeSpan.FromSeconds(1));
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
