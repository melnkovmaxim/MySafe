using MySafe.Services.Abstractions;
using MySafe.Views;
using System;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Repositories.Abstractions;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Prism.Navigation;
using Xamarin.Essentials;

namespace MySafe.Services
{
    public class LoginService : ILoginService, ITransientService
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
    }
}
