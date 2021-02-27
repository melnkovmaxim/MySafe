using MediatR;
using MySafe.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Mediator.Documents.GetDocumentInfo
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
