using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Sheets.GetFile
{
    public class FileQuery: IRequest<byte[]>
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
