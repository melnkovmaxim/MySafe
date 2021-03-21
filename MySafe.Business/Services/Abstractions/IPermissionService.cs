using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fody;

namespace MySafe.Business.Services.Abstractions
{
    public interface IPermissionService
    {
        Task<bool> TryGetStorageWritePermissionAsync();
    }
}
