using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    public class AuthViewModel : ViewModelBase
    {
        public AuthPassword Password { get; set; }

        public bool IsRegistered { get; set; }

        public AuthViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Password = new AuthPassword();
        }

        public DelegateCommand LoadedCommand => new DelegateCommand(async () =>
        {
            var k = !string.IsNullOrEmpty(await SecureStorage.GetAsync("ApplicationPassword"));
            IsRegistered = k;
        });
          //  IsNotAuthorized = string.IsNullOrEmpty(await SecureStorage.GetAsync("ApplicationPassword")));

        public DelegateCommand FingerPrintCommand => new DelegateCommand(async () =>
        {
            var request = new AuthenticationRequestConfiguration("Вход в MySafe", string.Empty);
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            if (result.Authenticated)
            {
                await NavigationService.NavigateAsync(nameof(MainPage));
            }
            else
            {
                Vibration.Vibrate(TimeSpan.FromSeconds(0.5));
            }
        });

        public DelegateCommand<string> EnterNumberCommand => new DelegateCommand<string>(async (number) =>
        {
            Password.Push(int.Parse(number));
            var password = await Password.GetPassword();

            if (await SecureStorage.GetAsync("ApplicationPassword") == password && IsRegistered)
            {
                await NavigationService.NavigateAsync(nameof(MainPage));
            }
            else if (password.Length == 5 && IsRegistered)
            {
                Vibration.Vibrate(TimeSpan.FromSeconds(0.5));
            }
            else if (password.Length == 5 && !IsRegistered)
            {
                await SecureStorage.SetAsync("ApplicationPassword", password);
                await NavigationService.NavigateAsync(nameof(AuthPage));
            }
        });

        public DelegateCommand RemoveLastNumberCommand => new DelegateCommand(() =>
        {
            Password.Pop();
        });

        public DelegateCommand RestorePasswordCommand => new DelegateCommand(async () =>
        {
            await SecureStorage.SetAsync("ApplicationPassword", string.Empty);
            await NavigationService.NavigateAsync(nameof(AuthPage));
        });
    }
}
