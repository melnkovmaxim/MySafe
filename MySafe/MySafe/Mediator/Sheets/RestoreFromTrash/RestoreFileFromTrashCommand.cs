using MediatR;
using MySafe.Presentation.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Sheets.RestoreFromTrash
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
