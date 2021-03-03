using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.GetFile
{
    public class FileQuery: IRequest<SheetResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int SheetId { get; set; }

        public FileQuery(JwtSecurityToken jwtToken, int sheetId)
        {
            JwtToken = jwtToken;
            SheetId = sheetId;
        }
    }
}
