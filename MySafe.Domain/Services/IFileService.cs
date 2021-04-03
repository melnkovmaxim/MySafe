using System.Threading.Tasks;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Enums;

namespace MySafe.Domain.Services
{
    public interface IFileService
    {
        Task<bool> TryDownloadAndSaveIfNotExist(int attachmentId, AttachmentTypeEnum attachmentType, string fileName,
            string fileExtension);

        Task<bool> TryDownloadAndSaveFile(int attachmentId, AttachmentTypeEnum attachmentType, string fileName,
            string fileExtension);

        Task<bool> TrySaveFileToDeviceMemoryAsync(string fileName, string fileExtension, byte[] bytes);
        Task<IEntity> DownloadFileAsync(int attachmentId, AttachmentTypeEnum attachmentType);
        bool TryGetFilePathOnDevice(out string filePath);
        string GetFileNameWithExtension(string fileName, string fileExtension);
        string GetFullPathFileOnDevice(string fileName, string fileExtension);
    }
}