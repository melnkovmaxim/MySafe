using MySafe.Domain.Services;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Commands;
using Prism.Navigation;
using System;

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

        public IPasswordManagerService PasswordManager { get; }
        public bool IsRegistered { get; set; }

        public DeviceAuthViewModel(INavigationService navigationService, IPasswordManagerService passwordManager,
            IDeviceAuthService deviceAuthService, IAuthService authService)
            : base(navigationService, authService)
        {
            PasswordManager = passwordManager;
            _deviceAuthService = deviceAuthService;
            _authService = authService;

            _actionOnLogin = Login;
            _actionOnRegister = async () => await _navigationService.NavigateAsync(nameof(DeviceAuthPage));

            RemoveLastNumberCommand = new DelegateCommand(() => PasswordManager.RemoveLast());
            FingerPrintScanCommand = new DelegateCommand(FingerPrintScan);
            RestorePasswordCommand = new DelegateCommand(RestorePassword);
            NumberInputCommand = new DelegateCommand<string>(NumberInput);
        }

        private async void Login()
        {
            var isAuthorized = await _authService.IsAuthorized();

            await _navigationService.NavigateAsync(isAuthorized
                ? nameof(MainPage)
                : nameof(SignInPage));
        }

        protected override async void DoAfterNavigatedTo()
        {
            IsRegistered = await _deviceAuthService.IsRegistered();
            await _deviceAuthService.TryLoginWithPrintScanAsync(_actionOnLogin);
        }

        private async void FingerPrintScan()
        {
            await _deviceAuthService.TryLoginWithPrintScanAsync(_actionOnLogin);
        }

        private async void NumberInput(string number)
        {
            if (!PasswordManager.TryAdd(number) || PasswordManager.PasswordLength != PasswordManager.PasswordMaxLength)
                return;

            if (IsRegistered)
            {
                var isSuccessfulLogin = await _deviceAuthService.TryLoginAsync(PasswordManager.Password, _actionOnLogin);

                if (!isSuccessfulLogin) PasswordManager.Clear();
            }
            else
            {
                await _deviceAuthService.RegisterAsync(PasswordManager.Password, _actionOnRegister);
            }
        }

        private async void RestorePassword()
        {
            await _navigationService.NavigateAsync(nameof(SignInPage));
        }
    }
}