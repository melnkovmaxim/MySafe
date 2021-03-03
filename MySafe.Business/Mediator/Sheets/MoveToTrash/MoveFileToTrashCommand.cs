using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.MoveToTrash
{
    public class MoveFileToTrashCommand: IRequest<SheetResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int SheetId { get; set; } // attachment 

        public MoveFileToTrashCommand(JwtSecurityToken jwtToken, int sheetId)
        {
            JwtToken = jwtToken;
            SheetId = sheetId;
        }
    }
}
