using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.RestoreTrashDocumentCommand
{
    /// <summary>
    /// Восстановить документ из корзины
    /// </summary>
    public class RestoreTrashDocumentCommand: AuthorizedRequestBase<Document>
    {
        public int DocumentId { get; set; }

        public RestoreTrashDocumentCommand(string jwtToken) : base(jwtToken)
        {
        }
    }
}
