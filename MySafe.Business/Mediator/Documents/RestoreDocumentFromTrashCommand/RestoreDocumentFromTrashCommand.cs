using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.RestoreDocumentFromTrashCommand
{
    /// <summary>
    /// Восстановить документ из корзины
    /// </summary>
    public class RestoreDocFromTrashCommand: AuthorizedRequestBase<DocumentResponse>
    {
        public int DocumentId { get; set; }

        public RestoreDocFromTrashCommand(string jwtToken) : base(jwtToken)
        {
        }
    }
}
