using System;
using System.IO;
using System.Threading.Tasks;
using Fody;
using MediatR;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Core.Enums;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
using MySafe.Services.Mediator.Images.OriginalImageQuery;
using MySafe.Services.Mediator.Sheets.OriginalSheetQuery;

namespace MySafe.Services.Xamarin
{
    [ConfigureAwait(false)]
    public class FileService : IFileService
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
                return await _mediator.Send(new OriginalImageQuery(attachmentId));

            return await _mediator.Send(new OriginalSheetQuery(attachmentId));
        }

        public Task<bool> TryDownloadAndSaveIfNotExist(int attachmentId, AttachmentTypeEnum attachmentType,
            string fileName,
            string fileExtension)
        {
            var path = GetFullPathFileOnDevice(fileName, fileExtension);

            if (!File.Exists(path))
                return TryDownloadAndSaveFile(attachmentId, attachmentType, fileName, fileExtension);

            return Task.FromResult(true);
        }

        public async Task<bool> TryDownloadAndSaveFile(int attachmentId, AttachmentTypeEnum attachmentType,
            string fileName, string fileExtension)
        {
            var downloadResult = await DownloadFileAsync(attachmentId, attachmentType);

            if (downloadResult?.HasError != false) return false;

            var saveResult = await TrySaveFileToDeviceMemoryAsync(fileName, fileExtension, downloadResult.FileBytes);

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