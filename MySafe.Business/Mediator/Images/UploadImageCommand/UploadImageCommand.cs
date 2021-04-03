using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.UploadImageCommand
{
    /// <summary>
    ///     Загрузка изображения
    /// </summary>
    public class UploadImageCommand : RequestUploadBase<Image>
    {
        public UploadImageCommand(int documentId, string fileName, string contentType, byte[] imageBytes)
        {
            DocumentId = documentId;
            FileName = fileName;
            ContentType = contentType;
            FileBytes = imageBytes;
        }

        public int DocumentId { get; }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => $"/api/v1/images?document_id={DocumentId}";
        public override string FileName { get; }
        public override string ContentType { get; }
        public override byte[] FileBytes { get; }
    }
}