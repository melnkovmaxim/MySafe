using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.GetDocumentInfo
{
    public class DocumentInfoQuery : IRequest<DocumentResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int FileId { get; set; }

        public DocumentInfoQuery(JwtSecurityToken jwtToken, int fileId)
        {
            JwtToken = jwtToken;
            FileId = fileId;
        }
    }
}
