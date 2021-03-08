using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Business.Mediator.Images.UploadImage
{
    public class UploadImageCommandHandler: IRequestHandler<UploadImageCommand, IRestResponse>
    {
        private readonly IRestClient _restClient;
        
        public UploadImageCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IRestResponse> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var httpRequest = new RestRequest($"/api/v1/images", Method.POST);
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            httpRequest.AddFile("file", request.ImageBytes, request.FileName, request.ContentType);
            //httpRequest.AddParameter(pickerResult.FileName, Convert.ToBase64String(bytes));
            //httpRequest.AddParameter(pickerResult.FileName, bytes);
            httpRequest.AddParameter("document_id", request.DocumentId);

            httpRequest.AlwaysMultipartFormData = true;
            var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken);
            return response;
        }
    }
}
