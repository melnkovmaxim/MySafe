using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestSharp;

namespace MySafe.Business.Mediator.Images.UploadImage
{
    public class UploadImageCommand: IRequest<IRestResponse>
    {
        public JwtSecurityToken JwtToken { get; }
        public int DocumentId { get; }
        public string FileName { get; }
        public string ContentType { get; }
        public byte[] ImageBytes { get; }

        public UploadImageCommand(JwtSecurityToken jwtToken, int documentId, string fileName, string contentType, byte[] imageBytes)
        {
            JwtToken = jwtToken;
            DocumentId = documentId;
            FileName = fileName;
            ContentType = contentType;
            ImageBytes = imageBytes;
        }
    }
}
