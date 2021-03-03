using Fody;
using MySafe.Core;
using MySafe.Services.Abstractions;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Repositories.Abstractions;
using Xamarin.Essentials;

namespace MySafe.Services
{
    public class DeviceAuthService : IDeviceAuthService, ITransientService
    {
        private const string FINGER_PRINT_SCAN_TITLE = "Вход в MySafe";
        private readonly ISecureStorageRepository _secureStorage;

        public DeviceAuthService(ISecureStorageRepository secureStorage)
        {
            _secureStorage = secureStorage;
        }

        public async Task<bool> TryLoginAsync(string password, Action actionOnLogin, TimeSpan vibrationDuration)
        {
            await Task.Run(() => Thread.Sleep(500));

            var correctPassword = await _secureStorage.GetLocalPasswordAsync();

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
            if (password.Length == MySafeApp.Resources.RequiredLengthDevicePwd)
            {
                await _secureStorage.SetLocalPasswordAsync(password);

                actionOnRegister?.Invoke();
            }
            
            await Task.Run(() => Thread.Sleep(500));
        }
        
        [ConfigureAwait(false)]
        public async Task Logout()
        {
            await _secureStorage.RemovePasswordAsync();
        }
    }
}
