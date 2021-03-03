using MediatR;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Essentials;

namespace MySafe.Presentation.Mediator.Sheets.UploadFile
{
    public class UploadFileCommand: IRequest<IRestResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int DocumentId { get; set; }
        public FileResult FilePickerResult { get; set; }

        public UploadFileCommand(JwtSecurityToken jwtToken, int documentId, FileResult filePickerResult)
        {
            JwtToken = jwtToken;
            DocumentId = documentId;
            FilePickerResult = filePickerResult;
        }
    }
}
