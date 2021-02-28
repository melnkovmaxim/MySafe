using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Sheets.MoveToTrash
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
