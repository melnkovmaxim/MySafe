using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Fody;
using MediatR;
using MySafe.Core.Dto;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Enums;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
using MySafe.Services.Mediator.Images.OriginalImageQuery;
using MySafe.Services.Mediator.Sheets.OriginalSheetQuery;
using MySafe.Services.Mediator.Sheets.SheetPdfFormatQuery;
using MySafe.Services.Services;
using Plugin.Printing;
using Xamarin.Essentials;

namespace MySafe.Services.Xamarin
{
    [ConfigureAwait(false)]
    public class FileService : IFileService
    {
        private const string DEFAULT_IMAGE_EXTENSION = ".jpeg";
        private readonly IMediator _mediator;
        private readonly IStoragePathesRepository _storagePathesRepository;
        private readonly IFileRestService _fileRestService;
        private readonly IMapper _mapper;

        public FileService(
            IMediator mediator, 
            IStoragePathesRepository storagePathesRepository, 
            IFileRestService fileRestService,
            IMapper mapper)
        {
            _mediator = mediator;
            _storagePathesRepository = storagePathesRepository;
            _fileRestService = fileRestService;
            _mapper = mapper;
        }

        public async Task<bool> TryPrintFileAsync(int fileId, AttachmentTypeEnum fileType, string fileName, string fileExtension)
        {
            var printingMessage = $"Printing {fileId + fileName}";
            var path = GetFullPathFileOnDevice(fileName, fileExtension);

            if (!File.Exists(path))
            {
                var isDownloaded = await TryDownloadAndSaveFile(fileId, fileType, fileName, fileExtension);
                if (!isDownloaded) return false;
            }

            if (fileType == AttachmentTypeEnum.Image)
            {
                var bytes = await File.ReadAllBytesAsync(path);

                await CrossPrinting.Current.PrintImageFromByteArrayAsync(bytes, new PrintJobConfiguration(printingMessage));

                return true;
            }

            Stream stream;

            if (fileExtension == ".pdf")
            {
                stream = File.OpenRead(path);
            }
            else
            {
                var result = await _mediator.Send(new SheetPdfFormatQuery(fileId));

                if (result.HasError) return false;

                stream = new MemoryStream(result.FileBytes);
            }

            _ = await CrossPrinting.Current.PrintPdfFromStreamAsync(stream, new PrintJobConfiguration(printingMessage));
            await stream.DisposeAsync();

            return true;
        }

        public async Task<FileResultDto> GetPickedFileResult()
        {
            var fileResult = await FilePicker.PickAsync(PickOptions.Default);
            return _mapper.Map<FileResultDto>(fileResult);
        }

        public async Task<byte[]> GetFileBytesFromStream(Stream stream)
        {
            await using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            return memoryStream.GetBuffer();
        }

        public async Task<bool> TryDownloadAndSaveFile(int fileId, AttachmentTypeEnum fileType, string fileName, string fileExtension)
        {
            var downloadResult = await _fileRestService.DownloadAsync(fileId, fileType);

            if (downloadResult?.HasError != false) return false;

            var path = GetFullPathFileOnDevice(fileName, fileExtension);
            await File.WriteAllBytesAsync(path, downloadResult.FileBytes);

            return true;
        }

        public string GetFullPathFileOnDevice(string fileName, string fileExtension)
        {
            var downloadPath = _storagePathesRepository.DownloadPath;
            var fullFileName = fileName + (fileExtension ?? DEFAULT_IMAGE_EXTENSION);
            var result = Path.Combine(downloadPath, fullFileName);

            return result;
        }

        public async Task<bool> TryOpenFileAsync(int fileId, AttachmentTypeEnum fileType, string fileName, string fileExtension)
        {
            var path = GetFullPathFileOnDevice(fileName, fileExtension);

            if (!File.Exists(path))
            {
                var isDownloaded = await TryDownloadAndSaveFile(fileId, fileType, fileName, fileExtension);
                if (!isDownloaded) return false;
            }
            
            var readOnlyFile = new ReadOnlyFile(path);
            var openFileRequest = new OpenFileRequest(fileName, readOnlyFile);

            await Launcher.OpenAsync(openFileRequest);

            return true;
        }
    }
}