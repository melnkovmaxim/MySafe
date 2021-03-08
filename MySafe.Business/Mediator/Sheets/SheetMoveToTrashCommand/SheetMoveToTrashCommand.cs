using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.SheetMoveToTrashCommand
{
    /// <summary>
    /// Отправить файл в корзину
    /// </summary>
    public class SheetMoveToTrashCommand: IRequest<Sheet>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int SheetId { get; set; } // attachment 

        public SheetMoveToTrashCommand(JwtSecurityToken jwtToken, int sheetId)
        {
            JwtToken = jwtToken;
            SheetId = sheetId;
        }
    }
}
