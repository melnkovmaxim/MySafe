using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.RestoreTrashSheetCommand
{
    /// <summary>
    /// Восстановить файл из корзины
    /// </summary>
    public class RestoreTrashSheetCommand: IRequest<Sheet>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int SheetId { get; set; } // attachment 

        public RestoreTrashSheetCommand(JwtSecurityToken jwtToken, int sheetId)
        {
            JwtToken = jwtToken;
            SheetId = sheetId;
        }
    }
}
