using System;
using System.Threading.Tasks;

namespace MySafe.Core.Interfaces.Services
{
    public interface IDeviceAuthService
    {
        Task<bool> TryLoginAsync(string password, Action actionOnLogin);
        Task<bool> TryLoginWithPrintScanAsync(Action actionOnLogin);
        Task RegisterAsync(string password, Action actionOnRegister);
        Task<bool> IsRegistered();
        Task Logout();
    }
}