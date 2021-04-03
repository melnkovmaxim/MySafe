using System;
using System.Threading.Tasks;

namespace MySafe.Domain.Services
{
    public interface IDeviceAuthService
    {
        Task<bool> TryLoginAsync(string password, Action actionOnLogin, TimeSpan vibrationDuration);
        Task<bool> TryLoginWithPrintScanAsync(Action actionOnLogin, TimeSpan vibrationDuration);
        Task RegisterAsync(string password, Action actionOnRegister);
        Task<bool> IsRegistered();
        Task Logout();
    }
}