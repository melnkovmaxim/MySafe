using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.OriginalSheetQuery
{
    /// <summary>
    /// Получение исходного файла
    /// </summary>
    public class OriginalSheetQuery: IRequest<Sheet>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int SheetId { get; set; }

        public OriginalSheetQuery(JwtSecurityToken jwtToken, int sheetId)
        {
            JwtToken = jwtToken;
            SheetId = sheetId;
        }
    }
}
