using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.DestroyTrashSheetCommand
{
    /// <summary>
    /// Уничтожить файл в корзине
    /// </summary>
    public class DestroyTrashSheetCommand: IRequest<Sheet>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int SheetId { get; set; } // attachment 

        public DestroyTrashSheetCommand(JwtSecurityToken jwtToken, int sheetId)
        {
            JwtToken = jwtToken;
            SheetId = sheetId;
        }
    }
}
