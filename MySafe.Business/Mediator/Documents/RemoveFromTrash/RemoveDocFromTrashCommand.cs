using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.RemoveFromTrash
{
    public class RemoveDocFromTrashCommand: IRequest<DocumentResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int DocumentId { get; set; }

        public RemoveDocFromTrashCommand(JwtSecurityToken jwtToken, int documentId)
        {
            JwtToken = jwtToken;
            DocumentId = documentId;
        }
    }
}
