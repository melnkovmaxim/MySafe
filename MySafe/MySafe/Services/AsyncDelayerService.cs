using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core;
using MySafe.Services.Abstractions;

namespace MySafe.Services
{
    public class AsyncDelayerService : IAsyncDelayerService
    {
        public Task Delay()
        {
            return Task.Delay(MySafeApp.Resources.DefaultTaskDelay);
        }
    }
}
