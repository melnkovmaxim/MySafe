using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.RestoreFromTrash
{
    public class RestoreFileFromTrashCommand: IRequest<SheetResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int SheetId { get; set; } // attachment 

        public RestoreFileFromTrashCommand(JwtSecurityToken jwtToken, int sheetId)
        {
            JwtToken = jwtToken;
            SheetId = sheetId;
        }
    }
}
