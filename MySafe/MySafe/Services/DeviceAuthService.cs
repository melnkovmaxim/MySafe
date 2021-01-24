using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<bool> TryLoginAsync(string password, Action actionOnLogin, TimeSpan vibrationDuration)
        {
            await Task.Run(() => Thread.Sleep(500));

            var correctPassword = await Ioc.Resolve<ISecureStorageRepository>().GetLocalPasswordAsync();

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

        public async Task RegisterAsync(string password, int requiredLength, Action actionOnRegister)
        {
            if (password.Length == requiredLength)
            {
                await Ioc.Resolve<ISecureStorageRepository>().SetLocalPasswordAsync(password);
                actionOnRegister?.Invoke();
            }
            
            await Task.Run(() => Thread.Sleep(500));
        }
    }
}
