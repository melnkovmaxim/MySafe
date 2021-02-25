using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.FileInfo
{
    public class FileInfoQuery : IRequest<FileResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int FileId { get; set; }

        public FileInfoQuery(JwtSecurityToken jwtToken, int fileId)
        {
            JwtToken = jwtToken;
            FileId = fileId;
        }
    }
}
