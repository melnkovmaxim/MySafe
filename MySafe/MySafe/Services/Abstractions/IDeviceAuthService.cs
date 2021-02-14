using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Services.Abstractions
{
    public interface IDeviceAuthService
    {
        Task<bool> TryLoginAsync(string password, Action actionOnLogin = null);
        Task<bool> TryLoginWithPrintScanAsync(Action actionOnLogin = null);
        Task RegisterAsync(string password, Action actionOnRegister = null);
    }
}
