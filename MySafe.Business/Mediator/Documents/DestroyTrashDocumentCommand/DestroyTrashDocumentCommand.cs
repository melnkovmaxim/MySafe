using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.DestroyTrashDocumentCommand
{
    /// <summary>
    /// Уничтожить документ в корзине
    /// </summary>
    public class DestroyTrashDocumentCommand: AuthorizedRequestBase<DocumentResponse>
    {
        public int DocumentId { get; set; }

        public DestroyTrashDocumentCommand(string jwtToken, int documentId) : base(jwtToken)
        {
            DocumentId = documentId;
        }
    }
}
