using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.Sheets.UploadFile
{
    public class UploadFileCommandHandler: IRequestHandler<UploadFileCommand, IRestResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public UploadFileCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<IRestResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            await using var stream = await request.FilePickerResult.OpenReadAsync();
            await using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            var bytes= memoryStream.ToArray();
            var pickerResult = request.FilePickerResult;

            var httpRequest = new RestRequest($"/api/v1/documents/{request.DocumentId}/sheets", Method.POST);
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            httpRequest.AddFileBytes(Path.GetFileNameWithoutExtension(pickerResult.FileName), bytes, pickerResult.FileName, pickerResult.ContentType);
            //httpRequest.AddParameter("Content-Disposition", $"form-data; name=\"file\"; filename=$\"{request.FileName}+.xlsx\"");
            //httpRequest.AddOrUpdateParameter("Content-Type", $"'application\\xlsx'");

            httpRequest.AlwaysMultipartFormData = true;

            var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken);
            return response;
        }
    }
}
