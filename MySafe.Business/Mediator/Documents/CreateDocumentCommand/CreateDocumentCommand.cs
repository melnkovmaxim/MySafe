using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Documents.CreateDocumentCommand
{
    /// <summary>
    /// Создать новый документ
    /// </summary>
    public class CreateDocumentCommand: AuthorizedRequestBase<Document>
    {
        public override Method RequestMethod => Method.POST;
        public override string RequestResource => $"/api/v1/folders/{FolderId}/documents";

        public int FolderId { get; set; }

        public CreateDocumentCommand(string jwtToken, int folderId) : base(jwtToken)
        {
            FolderId = folderId;
        }

    }
}
