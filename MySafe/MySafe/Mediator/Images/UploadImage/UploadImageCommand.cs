using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestSharp;
using Xamarin.Essentials;

namespace MySafe.Mediator.Images.UploadImage
{
    public class UploadImageCommand: IRequest<IRestResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int DocumentId { get; set; }
        public FileResult FilePickerResult { get; set; }

        public UploadImageCommand(JwtSecurityToken jwtToken, int documentId, FileResult filePickerResult)
        {
            JwtToken = jwtToken;
            DocumentId = documentId;
            FilePickerResult = filePickerResult;
        }
    }
}
