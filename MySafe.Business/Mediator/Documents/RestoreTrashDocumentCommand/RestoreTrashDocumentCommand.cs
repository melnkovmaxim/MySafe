using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.RestoreTrashDocumentCommand
{
    /// <summary>
    ///     Восстановить документ из корзины
    /// </summary>
    public class RestoreTrashDocumentCommand : BearerRequestBase<Document>
    {
        public RestoreTrashDocumentCommand(int documentId)
        {
            DocumentId = documentId;
        }

        public int DocumentId { get; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/documents/{DocumentId}/restore";
    }
}