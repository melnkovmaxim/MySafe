using System.Threading.Tasks;
using Fody;
using MySafe.Core.Interfaces.Services;
using Xamarin.Essentials;

namespace MySafe.Services.Xamarin
{
    [ConfigureAwait(false)]
    public class PermissionService : IPermissionService
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