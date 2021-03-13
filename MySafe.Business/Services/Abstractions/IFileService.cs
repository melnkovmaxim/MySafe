using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Core.Enums;

namespace MySafe.Business.Services.Abstractions
{
    public interface IFileService
    {
        Task<bool> TryDownloadAndSaveFile(Attachment attachment);
        Task<bool> TrySaveFileToDeviceMemoryAsync(string fileName, string fileExtension, byte[] bytes);
        Task<IResponse> DownloadFileAsync(int attachmentId, AttachmentTypeEnum attachmentType);
        bool TryGetFilePathOnDevice(out string filePath);
        string GetFileNameWithExtension(string fileName, string fileExtension);
        string GetFullPathFileOnDevice(string fileName, string fileExtension);
    }
}
