using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fody;
using MySafe.Core;
using MySafe.Repositories.Abstractions;
using MySafe.Services.Abstractions;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials;

namespace MySafe.Services
{
    public class DeviceAuthService : IDeviceAuthService, ITransientService
    {
        private const string FINGER_PRINT_SCAN_TITLE = "Вход в MySafe";
        private readonly ISecureStorageRepository _secureStorage;
        private readonly IAsyncDelayerService _delayerService;

        public DeviceAuthService(ISecureStorageRepository secureStorage, IAsyncDelayerService delayerService)
        {
            _secureStorage = secureStorage;
            _delayerService = delayerService;
        }

        public async Task<bool> TryLoginAsync(string password, Action actionOnLogin = null)
        {
            await _delayerService.Delay();

            var correctPassword = await _secureStorage.GetLocalPasswordAsync();

            if (password == correctPassword)
            {
                actionOnLogin?.Invoke();
                return true;
            }

            if (correctPassword?.Length == password.Length)
            {
                Vibration.Vibrate(MySafeApp.Resources.DefaultVibrationDuration);
            }

            return false;
        }

        public async Task<bool> TryLoginWithPrintScanAsync(Action actionOnLogin = null)
        {
            var request = new AuthenticationRequestConfiguration(FINGER_PRINT_SCAN_TITLE, string.Empty);
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);

            if (result.Authenticated)
            {
                actionOnLogin?.Invoke();
                return true;
            }

            Vibration.Vibrate(MySafeApp.Resources.DefaultVibrationDuration);

            return false;
        }
        
        [ConfigureAwait(false)]
        public async Task RegisterAsync(string password, Action actionOnRegister = null)
        {
            if (password.Length == MySafeApp.Resources.RequiredLengthDevicePwd)
            {
                await _secureStorage.SetLocalPasswordAsync(password);

                actionOnRegister?.Invoke();
            }

            await _delayerService.Delay();
        }
    }
}
