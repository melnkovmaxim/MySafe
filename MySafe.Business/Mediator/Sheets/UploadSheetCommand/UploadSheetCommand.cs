using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.UploadSheetCommand
{
    /// <summary>
    ///     Загрузка файла
    /// </summary>
    public class UploadSheetCommand : RequestUploadBase<Sheet>
    {
        public UploadSheetCommand(int documentId, string fileName, string contentType, byte[] fileBytes)
        {
            DocumentId = documentId;
            FileName = fileName;
            ContentType = contentType;
            FileBytes = fileBytes;
        }

        public int DocumentId { get; set; }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => $"/api/v1/documents/{DocumentId}/sheets";
        public override string FileName { get; }
        public override string ContentType { get; }
        public override byte[] FileBytes { get; }
    }
}