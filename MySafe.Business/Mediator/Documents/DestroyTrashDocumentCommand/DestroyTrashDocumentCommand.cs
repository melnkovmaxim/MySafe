using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.DestroyTrashDocumentCommand
{
    /// <summary>
    ///     Уничтожить документ в корзине
    /// </summary>
    public class DestroyTrashDocumentCommand : BearerRequestBase<Document>
    {
        public DestroyTrashDocumentCommand(int documentId)
        {
            DocumentId = documentId;
        }

        public int DocumentId { get; set; }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/documents/{DocumentId}";
    }
}