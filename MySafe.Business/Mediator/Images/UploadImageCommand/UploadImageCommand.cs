using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Images.UploadImageCommand
{
    /// <summary>
    /// Загрузка изображения
    /// </summary>
    public class UploadImageCommand: RequestUploadBase<Image>
    {
        public int DocumentId { get; }

        public UploadImageCommand(int documentId, string fileName, string contentType, byte[] imageBytes) 
        {
            DocumentId = documentId;
            FileName = fileName;
            ContentType = contentType;
            FileBytes = imageBytes;
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => $"/api/v1/images?document_id={DocumentId}";
        public override string FileName { get; }
        public override string ContentType { get; }
        public override byte[] FileBytes { get; }
    }
}
