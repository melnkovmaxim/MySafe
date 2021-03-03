using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.RestoreFromTrash
{
    public class RestoreDocFromTrashCommand: IRequest<DocumentResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int DocumentId { get; set; }

        public RestoreDocFromTrashCommand(JwtSecurityToken jwtToken, int documentId)
        {
            JwtToken = jwtToken;
            DocumentId = documentId;
        }
        
    }
}
