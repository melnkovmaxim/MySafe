using System.IO;
using System.Threading.Tasks;
using MySafe.Core.Dto;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Enums;

namespace MySafe.Domain.Services
{
    public interface IFileService
    {
        Task<bool> TryDownloadAndSaveFile(int fileId, AttachmentTypeEnum fileType, string fileName,
            string fileExtension);
        string GetFullPathFileOnDevice(string fileName, string fileExtension);
        Task<bool> TryOpenFileAsync(int fileId, AttachmentTypeEnum fileType, string fileName, string fileExtension);
        Task<FileResultDto> GetPickedFileResult();
        Task<byte[]> GetFileBytesFromStream(Stream stream);
        Task<bool> TryPrintFileAsync(int fileId, AttachmentTypeEnum fileType, string fileName, string fileExtension);
    }
}