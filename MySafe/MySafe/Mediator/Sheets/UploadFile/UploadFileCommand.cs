using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;
using RestSharp;

namespace MySafe.Mediator.Sheets.UploadFile
{
    public class UploadFileCommand: IRequest<IRestResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public UploadFileCommand(JwtSecurityToken jwtToken, int documentId, string fileName, byte[] file)
        {
            JwtToken = jwtToken;
            DocumentId = documentId;
            FileName = fileName;
            File = file;
        }
    }
}
