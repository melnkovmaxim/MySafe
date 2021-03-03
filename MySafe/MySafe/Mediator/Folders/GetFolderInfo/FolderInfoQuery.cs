using MediatR;
using MySafe.Presentation.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Folders.GetFolderInfo
{
    public class FolderInfoQuery : IRequest<FolderResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int DocumentId { get; set; }

        public FolderInfoQuery(JwtSecurityToken jwtToken, int documentId)
        {
            JwtToken = jwtToken;
            DocumentId = documentId;
        }
    }
}
