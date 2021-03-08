using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MySafe.Business.Mediator.Sheets.UploadFile
{
    public class UploadFileCommandHandler: IRequestHandler<UploadFileCommand, IRestResponse>
    {
        private readonly IRestClient _restClient;
        
        public UploadFileCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IRestResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {

            var httpRequest = new RestRequest($"/api/v1/documents/{request.DocumentId}/sheets", Method.POST);
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            httpRequest.AddFile("file", request.FileBytes, request.FileName, request.ContentType);

            httpRequest.AlwaysMultipartFormData = true;
            var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken);
            return response;
        }
    }
}
