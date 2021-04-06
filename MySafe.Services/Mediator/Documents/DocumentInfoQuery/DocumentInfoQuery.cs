using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.DocumentInfoQuery
{
    /// <summary>
    ///     Получить информацию о документе
    /// </summary>
    public class DocumentInfoQuery : BearerRequestBase<DocumentEntity>
    {
        public DocumentInfoQuery(int documentId)
        {
            DocumentId = documentId;
        }

        public int DocumentId { get; }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"api/v1/documents/{DocumentId}";
    }
}