using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Business.Services.Abstractions
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
