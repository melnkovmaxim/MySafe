using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Folders.GetFolderInfo
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
