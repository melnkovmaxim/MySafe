using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Documents.DestroyTrashDocumentCommand
{
    /// <summary>
    /// Уничтожить документ в корзине
    /// </summary>
    public class DestroyTrashDocumentCommand: BearerRequestBase<Document>
    {
        public int DocumentId { get; set; }

        public DestroyTrashDocumentCommand(int documentId)
        {
            DocumentId = documentId;
        }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/documents/{DocumentId}";
    }
}
