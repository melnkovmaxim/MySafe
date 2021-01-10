using System;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Services.Abstractions;
using MySafe.Views;
using Prism.Navigation;
using Xamarin.Essentials;

namespace MySafe.Services
{
    public class RegisterService : IRegisterService, ITransientService
    {
        public async Task RegisterAsync(string password, int requiredLength, Action actionOnRegister)
        {
            if (password.Length == requiredLength)
            {
                await SecureStorage.SetAsync(App.Resources.PasswordPath, password);
                actionOnRegister?.Invoke();
            }
            
            await Task.Run(() => Thread.Sleep(500));
        }
    }
}
