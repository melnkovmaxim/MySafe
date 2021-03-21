using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fody;
using MySafe.Business.Services.Abstractions;
using Xamarin.Essentials;

namespace MySafe.Presentation.Services
{
    [ConfigureAwait(false)]
    public class PermissionService: IPermissionService
    {
        public async Task<bool> TryGetStorageWritePermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();

                if (status != PermissionStatus.Granted) return false;
            }

            return true;
        }
    }
}
