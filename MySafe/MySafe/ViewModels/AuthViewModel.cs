﻿
using MySafe.Repositories.Abstractions;
using MySafe.Services.Abstractions;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using Prism.Commands;
using System;
using DelegateCommand = Prism.Commands.DelegateCommand;
using INavigationService = Prism.Navigation.INavigationService;

namespace MySafe.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        public DelegateCommand RemoveLastNumberCommand{ get; }
        public DelegateCommand LoadedCommand { get; }
        public DelegateCommand FingerPrintScanCommand { get; }
        public DelegateCommand RestorePasswordCommand { get; }
        public DelegateCommand<string> NumberInputCommand { get; }
        
        public IPasswordManagerService PasswordManager { get; }
        public bool IsRegistered { get; set; }

        private readonly IDeviceAuthService _deviceAuthService;
        private readonly TimeSpan _vibrationDuration;

        private readonly Action _actionOnLogin;
        private readonly Action _actionOnRegister;

        public AuthViewModel(INavigationService navigationService, IPasswordManagerService passwordManager, IDeviceAuthService deviceAuthService)
            :base(navigationService)
        {
            PasswordManager = passwordManager;
            _deviceAuthService = deviceAuthService;
            _vibrationDuration = TimeSpan.FromSeconds(0.2);

            LoadedCommand = new DelegateCommand(Loaded);
            RemoveLastNumberCommand = new DelegateCommand(() => PasswordManager.RemoveLast());
            FingerPrintScanCommand = new DelegateCommand(FingerPrintScan);
            RestorePasswordCommand = new DelegateCommand(RestorePassword);
            NumberInputCommand = new DelegateCommand<string>(NumberInput);

            _actionOnLogin = Login;
            _actionOnRegister = async () => await _navigationService.NavigateAsync(nameof(AuthPage));
        }

        private async void Login()
        {
            var storageRepository = Ioc.Resolve<ISecureStorageRepository>();
            var token = await storageRepository.GetJstTokenAsync();
            await _navigationService.NavigateAsync(IsValidToken(token)
                ? nameof(MainPage)
                : nameof(SignInPage));
        }

        private async void Loaded()
        {
            var passwordFromStorage = await Ioc.Resolve<ISecureStorageRepository>().GetLocalPasswordAsync();
            IsRegistered = !string.IsNullOrEmpty(passwordFromStorage);
        }

        private async void FingerPrintScan()
        {
            await _deviceAuthService.TryLoginWithPrintScanAsync(_actionOnLogin, _vibrationDuration);
        }

        private async void NumberInput(string number)
        {
            if (!PasswordManager.TryAdd(number) || PasswordManager.PasswordLength != PasswordManager.PasswordMaxLength)
                return;

            if (IsRegistered)
            {
                var isSuccessfulLogin = await Ioc.Resolve<IDeviceAuthService>()
                    .TryLoginAsync(PasswordManager.Password, _actionOnLogin, _vibrationDuration);

                if (!isSuccessfulLogin) PasswordManager.Clear();
            }
            else
            {
                await Ioc.Resolve<IDeviceAuthService>()
                    .RegisterAsync(PasswordManager.Password, _actionOnRegister);
            }
        }

        private async void RestorePassword()
        {
            var secureStorage = Ioc.Resolve<ISecureStorageRepository>();
            await secureStorage.RemovePasswordAsync();
            await secureStorage.RemoveToken();
            await _navigationService.NavigateAsync(nameof(AuthPage));
        }
    }
}
