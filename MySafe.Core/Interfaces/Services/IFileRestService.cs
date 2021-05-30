using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core.Dto;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Enums;

namespace MySafe.Core.Interfaces.Services
{
    public interface IFileRestService
    {
        Task<IEntity> MoveToTrashAsync(int attachmentId, AttachmentTypeEnum attachmentType);
        Task<IEntity> DownloadAsync(int attachmentId, AttachmentTypeEnum attachmentType);
        Task<IEntity> UploadAsync(int documentId, FileResultDto fileResult);
    }
}
