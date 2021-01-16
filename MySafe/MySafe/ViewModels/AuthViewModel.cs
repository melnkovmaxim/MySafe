
using MySafe.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using MediatR;
using MySafe.Helpers;
using MySafe.Mediator.Test;
using MySafe.Services;
using MySafe.Services.Abstractions;
using MySafe.ViewModels.Abstractions;
using NetStandardCommands;
using Prism.Commands;
using Xamarin.Essentials;
using DelegateCommand = Prism.Commands.DelegateCommand;
using INavigationService = Prism.Navigation.INavigationService;

namespace MySafe.ViewModels
{
    public class AuthViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILoginService _loginService;
        private readonly TimeSpan _vibrationDuration;
        
        private readonly Action _actionOnLogin;
        private readonly Action _actionOnRegister;

        private DelegateCommand _removeLastNumberCommand;
        private DelegateCommand _loadedCommand;
        private DelegateCommand _fingerPrintScanCommand;
        private DelegateCommand _restorePasswordCommand;
        private DelegateCommand<string> _numberInputCommand;

        public IPasswordManagerService PasswordManager { get; }
        public bool IsRegistered { get; set; }

        public AuthViewModel(INavigationService navigationService, IPasswordManagerService passwordManager, ILoginService loginService)
        {
            PasswordManager = passwordManager;
            _navigationService = navigationService;
            _loginService = loginService;
            _vibrationDuration = TimeSpan.FromSeconds(0.2);
            _actionOnLogin = async () => await NavigateHelper.NavigateAsync(navigationService, nameof(MainPage));
            _actionOnRegister = async () => await NavigateHelper.NavigateAsync(navigationService, nameof(AuthPage));
        }

        public DelegateCommand LoadedCommand => _loadedCommand ??= new DelegateCommand(async () =>
        {
            var passwordFromStorage = await SecureStorage.GetAsync(App.Resources.PasswordPath);
            IsRegistered = !string.IsNullOrEmpty(passwordFromStorage);
        });

        public DelegateCommand FingerPrintScanCommand => _fingerPrintScanCommand ??= new DelegateCommand(async () =>
        {
            await _loginService.TryLoginWithPrintScanAsync(_actionOnLogin, _vibrationDuration);
        });

        public DelegateCommand<string> NumberInputCommand => 
            _numberInputCommand ??= new DelegateCommand<string>(async (number) =>
        {
            if (!PasswordManager.TryAdd(number) || PasswordManager.PasswordLength != PasswordManager.PasswordMaxLength)
                return;

            if (IsRegistered)
            {
                var isSuccessfulLogin = await Ioc.Resolve<ILoginService>()
                    .TryLoginAsync(PasswordManager.Password, _actionOnLogin, _vibrationDuration);

                if (!isSuccessfulLogin) PasswordManager.Clear();
            }
            else
            {
                await Ioc.Resolve<IRegisterService>()
                    .RegisterAsync(PasswordManager.Password, PasswordManager.PasswordMaxLength, _actionOnRegister);
            }
        });

        public DelegateCommand RemoveLastNumberCommand => _removeLastNumberCommand ??= new DelegateCommand(() =>
        {
            PasswordManager.RemoveLast();
        });

        public DelegateCommand RestorePasswordCommand => _restorePasswordCommand ??= new DelegateCommand(async () =>
        {
            await SecureStorage.SetAsync(App.Resources.PasswordPath, string.Empty);
            await NavigateHelper.NavigateAsync(_navigationService, nameof(AuthPage));
        });
    }
}
