using AutoMapper;
using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MySafe.Presentation.Mediator.Sheets.UploadFile
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
            httpRequest.AddFile(Path.GetFileNameWithoutExtension(pickerResult.FileName), bytes, pickerResult.FileName, pickerResult.ContentType);
            //httpRequest.AddHeader("Content-Disposition", $"form-data; name=file; filename={pickerResult.FileName}");
            //httpRequest.AddHeader("Content-Type", pickerResult.ContentType);

            httpRequest.AlwaysMultipartFormData = true;
            var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken);
            return response;
        }
    }
}
