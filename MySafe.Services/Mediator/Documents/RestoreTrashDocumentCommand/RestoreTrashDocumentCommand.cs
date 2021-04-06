using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.RestoreTrashDocumentCommand
{
    /// <summary>
    ///     Восстановить документ из корзины
    /// </summary>
    public class RestoreTrashDocumentCommand : BearerRequestBase<DocumentEntity>
    {
        public RestoreTrashDocumentCommand(int documentId, int folderId)
        {
            DocumentId = documentId;
            FolderId = folderId;
        }

        public int DocumentId { get; }
        public int FolderId { get; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/documents/{DocumentId}/restore";
    }
}