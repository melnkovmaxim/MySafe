using MediatR;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.UploadSheetCommand
{
    /// <summary>
    /// Загрузка файла
    /// </summary>
    public class UploadSheetCommand: RequestUploadBase<Sheet>
    {
        public int DocumentId { get; set; }

        public UploadSheetCommand(string jwtToken, int documentId, string fileName, string contentType, byte[] fileBytes)
            : base(jwtToken)
        {
            DocumentId = documentId;
            FileName = fileName;
            ContentType = contentType;
            FileBytes = fileBytes;
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => $"/api/v1/documents/{DocumentId}/sheets";
        public override string FileName { get; }
        public override string ContentType { get; }
        public override byte[] FileBytes { get; }
    }
}
