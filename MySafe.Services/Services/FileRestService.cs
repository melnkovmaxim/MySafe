using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Core.Dto;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Enums;
using MySafe.Core.Interfaces.Services;
using MySafe.Services.Mediator.Images.ImageMoveToTrashCommand;
using MySafe.Services.Mediator.Images.OriginalImageQuery;
using MySafe.Services.Mediator.Images.UploadImageCommand;
using MySafe.Services.Mediator.Sheets.OriginalSheetQuery;
using MySafe.Services.Mediator.Sheets.SheetMoveToTrashCommand;
using MySafe.Services.Mediator.Sheets.UploadSheetCommand;

namespace MySafe.Services.Services
{
    public class FileRestService: IFileRestService
    {
        private readonly IMediator _mediator;

        public FileRestService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<IEntity> DownloadAsync(int attachmentId, AttachmentTypeEnum attachmentType)
        {
            if (attachmentType == AttachmentTypeEnum.Image)
            {
                return await _mediator.Send(new OriginalImageQuery(attachmentId));
            }

            return await _mediator.Send(new OriginalSheetQuery(attachmentId));
        }

        public async Task<IEntity> MoveToTrashAsync(int attachmentId, AttachmentTypeEnum attachmentType)
        {
            if (attachmentType == AttachmentTypeEnum.Image)
            {
                return await _mediator.Send(new ImageMoveToTrashCommand(attachmentId));
            }
                
            return await _mediator.Send(new SheetMoveToTrashCommand(attachmentId));
        }

        public async Task<IEntity> UploadAsync(int documentId, FileResultDto fileResult)
        {
            await using var stream = await fileResult.FileStream;
            var fileBytes = await GetFileBytesFromStream(stream);

            if (fileResult.ContentType.Split('/')[0] == "image")
            {
                return await _mediator.Send(new UploadImageCommand(documentId, fileResult.FileName, fileResult.ContentType, fileBytes));
            }
                
            return await _mediator.Send(new UploadSheetCommand(documentId, fileResult.FileName, fileResult.ContentType, fileBytes));
        }

        private async Task<byte[]> GetFileBytesFromStream(Stream stream)
        {
            await using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            return memoryStream.GetBuffer();
        }
    }
}
