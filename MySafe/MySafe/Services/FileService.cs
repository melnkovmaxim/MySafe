using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fody;
using MediatR;
using MySafe.Business.Mediator.Images.OriginalImageQuery;
using MySafe.Business.Mediator.Sheets.OriginalSheetQuery;
using MySafe.Business.Services.Abstractions;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Core.Enums;
using MySafe.Data.Abstractions;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.Services
{
    [ConfigureAwait(false)]
    public class FileService: IFileService
    {
        private const string DEFAULT_IMAGE_EXTENSION = ".jpeg";
        private readonly IMediator _mediator;
        private readonly IStoragePathesRepository _storagePathesRepository;

        public FileService(IMediator mediator, IStoragePathesRepository storagePathesRepository)
        {
            _mediator = mediator;
            _storagePathesRepository = storagePathesRepository;
        }

        public async Task<bool> TrySaveFileToDeviceMemoryAsync(string fileName, string fileExtension, byte[] bytes)
        {
            var path = GetFullPathFileOnDevice(fileName, fileExtension);

            try
            {
                await File.WriteAllBytesAsync(path, bytes);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<IResponse> DownloadFileAsync(int attachmentId, AttachmentTypeEnum attachmentType)
        {
            if (attachmentType == AttachmentTypeEnum.Image)
            {
                return await _mediator.Send(new OriginalImageQuery(attachmentId));
            }
                
            return await _mediator.Send(new OriginalSheetQuery(attachmentId));
        }

        public async Task<bool> TryDownloadAndSaveFile(Attachment attachment)
        {
            var downloadResult = await DownloadFileAsync(attachment.Id, AttachmentTypeEnum.Image);

            if (downloadResult?.HasError != false) return false;
            
            var saveResult = await TrySaveFileToDeviceMemoryAsync(attachment.Name, attachment.FileExtension, downloadResult.FileBytes);

            return saveResult;
        }

        public bool TryGetFilePathOnDevice(out string filePath)
        {
            throw new NotImplementedException();
        }

        public string GetFileNameWithExtension(string fileName, string fileExtension)
        {
            var result = fileName + (fileExtension ?? DEFAULT_IMAGE_EXTENSION);

            return result;
        }

        public string GetFullPathFileOnDevice(string fileName, string fileExtension)
        {
            var downloadPath = _storagePathesRepository.DownloadPath;
            var fullFileName = GetFileNameWithExtension(fileName, fileExtension);
            var result = Path.Combine(downloadPath, fullFileName);

            return result;
        }
    }
}
