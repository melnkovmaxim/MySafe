using System;
using System.Threading;
using System.Threading.Tasks;
using Fody;
using MySafe.Business.Services.Abstractions;
using MySafe.Core;
using MySafe.Data.Abstractions;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials;

namespace MySafe.Business.Services
{
    public class DeviceAuthService : IDeviceAuthService, ITransientService
    {
        private const string FINGER_PRINT_SCAN_TITLE = "Вход в MySafe";
        private readonly ISecureStorageRepository _secureStorageRepository;

        public DeviceAuthService(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }
        public async Task<bool> IsRegistered()
        {
            var devicePassword = await _secureStorageRepository.GetDevicePasswordAsync();

            return !string.IsNullOrEmpty(devicePassword);
        }

        public async Task<bool> TryLoginAsync(string password, Action actionOnLogin, TimeSpan vibrationDuration)
        {
            await Task.Run(() => Thread.Sleep(500));

            var correctPassword = await _secureStorageRepository.GetDevicePasswordAsync();

            if (password == correctPassword)
            {
                actionOnLogin?.Invoke();
                return true;
            }

            if (password.Length == correctPassword.Length)
            {
                Vibration.Vibrate(vibrationDuration);
            }

            return false;
        }

        public async Task<bool> TryLoginWithPrintScanAsync(Action actionOnLogin, TimeSpan vibrationDuration)
        {
            var request = new AuthenticationRequestConfiguration(FINGER_PRINT_SCAN_TITLE, string.Empty);
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);

            if (result.Authenticated)
            {
                actionOnLogin?.Invoke();
                return true;
            }

            Vibration.Vibrate(vibrationDuration);

            return false;
        }
        
        [ConfigureAwait(false)]
        public async Task RegisterAsync(string password, Action actionOnRegister)
        {
            if (password.Length == MySafeApp.Resources.DefaultApplicationPasswordLength)
            {
                await _secureStorageRepository.SetDevicePasswordAsync(password);

                actionOnRegister?.Invoke();
            }
            
            await Task.Run(() => Thread.Sleep(500));
        }
        
        [ConfigureAwait(false)]
        public async Task Logout()
        {
            await _secureStorageRepository.RemoveDevicePasswordAsync();
        }
    }
}
