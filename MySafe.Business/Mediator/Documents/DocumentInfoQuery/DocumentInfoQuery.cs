using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.DocumentInfoQuery
{
    public class DocumentInfoQuery: AuthorizedRequestBase<DocumentResponse>
    {
        public int FileId { get; set; }

        public DocumentInfoQuery(string jwtToken, int fileId) : base(jwtToken)
        {
            FileId = fileId;
        }
    }
}
