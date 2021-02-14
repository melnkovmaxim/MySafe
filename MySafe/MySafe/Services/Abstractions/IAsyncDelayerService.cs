using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Services.Abstractions
{
    public interface IAsyncDelayerService
    {
        Task Delay();
    }
}
