using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace MySafe.Services.Abstractions
{
    public interface ILoginService
    {
        Task<bool> TryLoginAsync(string password, Action actionOnLogin, TimeSpan vibrationDuration);
        Task<bool> TryLoginWithPrintScanAsync(Action actionOnLogin, TimeSpan vibrationDuration);
    }
}
