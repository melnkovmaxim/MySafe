using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.CreateDocumentCommand
{
    /// <summary>
    /// Создать новый документ
    /// </summary>
    public class CreateDocumentCommand: AuthorizedRequestBase<DocumentResponse>
    {
        public int FolderId { get; set; }

        public CreateDocumentCommand(string jwtToken, int folderId) : base(jwtToken)
        {
            FolderId = folderId;
        }

    }
}
