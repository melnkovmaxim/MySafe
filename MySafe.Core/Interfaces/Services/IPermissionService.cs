using System.Threading.Tasks;

namespace MySafe.Core.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<bool> TryGetStorageWritePermissionAsync();
    }
}