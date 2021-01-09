using MySafe.Models;
using MySafe.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using NetStandardCommands;
using Prism.Commands;
using Xamarin.Essentials;
using DelegateCommand = Prism.Commands.DelegateCommand;
using INavigationService = Prism.Navigation.INavigationService;

namespace MySafe.ViewModels
{
    // TODO: во вьюшке добавить статик ресурсы
    public class AuthViewModel : ViewModelBase
    {
        public AuthPassword Password { get; set; }
        public bool IsRegistered { get; set; }

        public AuthViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Password = new AuthPassword(5);
        }

        // TODO: вынести "applicationpassword" в другой класc/модуль
        public DelegateCommand LoadedCommand => _loadedCommand ??= new DelegateCommand(async () =>
        {
            var k = !string.IsNullOrEmpty(await SecureStorage.GetAsync("ApplicationPassword"));
            IsRegistered = k;
        });
        //  IsNotAuthorized = string.IsNullOrEmpty(await SecureStorage.GetAsync("ApplicationPassword")));

        private AsyncCommand<string> _navigateCommand;
        private DelegateCommand _restorePasswordCommand;
        private DelegateCommand _removeLastNumberCommand;
        private DelegateCommand _loadedCommand;
        private DelegateCommand _fingerPrintCommand;
        private DelegateCommand<string> _enterNumberCommand;

        public DelegateCommand FingerPrintCommand => _fingerPrintCommand ??= new DelegateCommand(async () =>
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

        // TODO: в сервис или медиатор, добавить флаг на одновременное выполнение только одной команды 
        public DelegateCommand<string> EnterNumberCommand =>
            _enterNumberCommand ??= new Prism.Commands.DelegateCommand<string>(async (number) =>
            {
                Password.Add(number);

                //if (IsRegistered)
                //{
                //    // mediator
                //}
                //else
                //{

                //}

                var password = await Password.GetPassword();

                if (await SecureStorage.GetAsync("ApplicationPassword") == password && IsRegistered)
                {
                    await NavigateCommand.ExecuteAsync(nameof(MainPage));
                    //await NavigationService.NavigateAsync($"NavigationPage/{nameof(MainPage)}?appModuleRefresh=OnInitialized");
                }
                else if (password.Length == Password.PasswordMaxLength && IsRegistered)
                {
                    Vibration.Vibrate(TimeSpan.FromSeconds(0.5));
                    Password.Clear();
                }
                else if (password.Length == Password.PasswordMaxLength && !IsRegistered)
                {
                    await SecureStorage.SetAsync("ApplicationPassword", password);
                    await NavigateCommand.ExecuteAsync(nameof(AuthPage));
                }
            });

        public DelegateCommand RemoveLastNumberCommand => 
            _removeLastNumberCommand ??= new DelegateCommand(() =>
        {
            Password.RemoveLast();
        });

        public DelegateCommand RestorePasswordCommand => 
            _restorePasswordCommand ??= new DelegateCommand(async () =>
        {
            await SecureStorage.SetAsync("ApplicationPassword", string.Empty);
            await NavigateCommand.ExecuteAsync(nameof(AuthPage));
        });

        public AsyncCommand<string> NavigateCommand =>
            _navigateCommand ??= new AsyncCommand<string>(async (page) => 
                await NavigationService.NavigateAsync(page), 
                canExecuteMethod: (s) => true, allowMultipleExecution: false);
    }
}
