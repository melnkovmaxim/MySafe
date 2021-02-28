using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Documents.RestoreFromTrash
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
