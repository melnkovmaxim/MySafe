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
    public class CreateDocumentCommand: BearerRequestBase<Document>
    {
        public int FolderId { get; set; }

        public CreateDocumentCommand(int folderId)
        {
            FolderId = folderId;
        }
        public override Method RequestMethod => Method.POST;
        public override string RequestResource => $"/api/v1/folders/{FolderId}/documents";
    }
}
