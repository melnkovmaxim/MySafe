using System;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Extensions;
using Prism.Commands;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class DeviceAuthViewModel : AuthorizedViewModelBase
    {
        private readonly Action _actionOnLoadedPage;

        private readonly Action _actionOnLogin;
        private readonly Action _actionOnRegister;

        private readonly IDeviceAuthService _deviceAuthService;
        private readonly IAuthService _authService;
        private readonly TimeSpan _vibrationDuration;

        public DeviceAuthViewModel(INavigationService navigationService, IPasswordManagerService passwordManager,
            IDeviceAuthService deviceAuthService, IAuthService authService)
            : base(navigationService, authService)
        {
            PasswordManager = passwordManager;
            _deviceAuthService = deviceAuthService;
            _authService = authService;
            _vibrationDuration = TimeSpan.FromSeconds(0.2);

            _actionOnLogin = Login;
            _actionOnRegister = async () => await _navigationService.NavigateAsync(nameof(DeviceAuthPage));

            RemoveLastNumberCommand = new DelegateCommand(() => PasswordManager.RemoveLast());
            FingerPrintScanCommand = new DelegateCommand(FingerPrintScan);
            RestorePasswordCommand = new DelegateCommand(RestorePassword);
            NumberInputCommand = new DelegateCommand<string>(NumberInput);
        }

        public DelegateCommand RemoveLastNumberCommand { get; }
        public DelegateCommand FingerPrintScanCommand { get; }
        public DelegateCommand RestorePasswordCommand { get; }
        public DelegateCommand<string> NumberInputCommand { get; }

        public IPasswordManagerService PasswordManager { get; }
        public bool IsRegistered { get; set; }

        private async void Login()
        {
            var storageRepository = Ioc.Resolve<ISecureStorageRepository>();
            var token = await storageRepository.GetAccessSecurityTokenAsync();
            await _navigationService.NavigateAsync(token.IsExpired()
                ? nameof(SignInPage)
                : nameof(MainPage));
        }

        protected override async void DoAfterNavigatedTo()
        {
            IsRegistered = await _deviceAuthService.IsRegistered();
            await _deviceAuthService.TryLoginWithPrintScanAsync(_actionOnLogin, _vibrationDuration);
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
            await _authService.SignOut();
            await _navigationService.NavigateAsync(nameof(SignInPage));
        }
    }
}