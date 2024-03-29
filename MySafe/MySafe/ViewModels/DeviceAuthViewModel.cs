﻿using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using MySafe.Core.Interfaces.Services;
using MySafe.Services.Services;

namespace MySafe.Presentation.ViewModels
{
    public class DeviceAuthViewModel : AuthorizedViewModelBase
    {
        private readonly IDeviceAuthService _deviceAuthService;
        private readonly IAuthService _authService;
        private readonly Action _actionOnLogin;
        private readonly Action _actionOnRegister;

        public DelegateCommand RemoveLastNumberCommand { get; }
        public DelegateCommand FingerPrintScanCommand { get; }
        public DelegateCommand RestorePasswordCommand { get; }
        public DelegateCommand<string> NumberInputCommand { get; }

        public Password Password { get; }
        public bool IsRegistered { get; set; }
        public bool IsExpiredAccessToken { get; set; }
        public bool IsLogged { get; set; }
        public bool IsNotLogged { get; set; }

        public DeviceAuthViewModel(INavigationService navigationService,
            IDeviceAuthService deviceAuthService, IAuthService authService)
            : base(navigationService, authService)
        {
            _deviceAuthService = deviceAuthService;
            _authService = authService;

            _actionOnLogin = Login;
            _actionOnRegister = async () => await _navigationService.NavigateAsync(nameof(DeviceAuthPage));
            
            Password = new Password();
            RemoveLastNumberCommand = new DelegateCommand(() => Password.RemoveLast());
            FingerPrintScanCommand = new DelegateCommand(FingerPrintScan);
            RestorePasswordCommand = new DelegateCommand(RestorePassword);
            NumberInputCommand = new DelegateCommand<string>(NumberInput);
        }

        private async void Login()
        {
            
            var isAuthorized = await _authService.IsAuthorized();
            IsLogged = true;
            await Task.Delay(50);
            await _navigationService.NavigateAsync(isAuthorized
                ? nameof(MainPage)
                : nameof(SignInPage));
        }

        protected override async void DoAfterNavigatedTo()
        {
            IsExpiredAccessToken = false;
            IsRegistered = await _deviceAuthService.IsRegistered();

            if (IsRegistered)
            {
                await _deviceAuthService.TryLoginWithPrintScanAsync(_actionOnLogin);
            }
        }

        protected override async void DoBeforeNavigatedTo()
        {
            IsExpiredAccessToken = await _authService.IsExpiredAccessToken();
        }

        private async void FingerPrintScan()
        {
            await _deviceAuthService.TryLoginWithPrintScanAsync(_actionOnLogin);
        }

        private async void NumberInput(string number)
        {
            if (!Password.TryAdd(number) || Password.PasswordLength != Password.PasswordMaxLength)
                return;

            if (IsRegistered)
            {
                var isSuccessfulLogin = await _deviceAuthService.TryLoginAsync(Password.PasswordStr, _actionOnLogin);

                if (!isSuccessfulLogin) { 
                    Password.Clear();
                    IsNotLogged = true;
                    await Task.Delay(2500);
                    IsNotLogged = false;
                }
            }
            else
            {
                await _deviceAuthService.RegisterAsync(Password.PasswordStr, _actionOnRegister);
            }
        }

        private async void RestorePassword()
        {
            await _navigationService.NavigateAsync(nameof(SignInPage));
        }
    }
}