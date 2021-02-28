using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.Images.UploadImage
{
    public class UploadImageCommandHandler: IRequestHandler<UploadImageCommand, IRestResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public UploadImageCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<IRestResponse> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            await using var stream = await request.FilePickerResult.OpenReadAsync();
            await using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            var bytes= memoryStream.ToArray();
            var pickerResult = request.FilePickerResult;

            var httpRequest = new RestRequest($"/api/v1/images", Method.POST);
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            httpRequest.AddFile(Path.GetFileNameWithoutExtension(pickerResult.FileName), bytes, pickerResult.FileName, pickerResult.ContentType);
            //httpRequest.AddParameter(pickerResult.FileName, Convert.ToBase64String(bytes));
            //httpRequest.AddParameter(pickerResult.FileName, bytes);
            httpRequest.AddParameter("document_id", request.DocumentId);

            httpRequest.AlwaysMultipartFormData = true;
            var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken);
            return response;
        }
    }
}
