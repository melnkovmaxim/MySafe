using System.Threading.Tasks;

namespace MySafe.Domain.Services
{
    public interface IPermissionService
    {
        Task<bool> TryGetStorageWritePermissionAsync();
    }
}