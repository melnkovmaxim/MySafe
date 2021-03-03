using AutoMapper;
using MediatR;
using MySafe.Presentation.Extensions;
using MySafe.Presentation.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;

namespace MySafe.Presentation.Mediator.Sheets.GetFile
{
    public class FileQueryHandler: IRequestHandler<FileQuery, SheetResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public FileQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<SheetResponse> Handle(FileQuery request, CancellationToken cancellationToken)
        {
            var httpRequest = new RestRequest($"/api/v1/sheets/{request.SheetId}/download", Method.GET);
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var response = await _restClient.GetResponseAsync<SheetResponse>(httpRequest, cancellationToken);

            return response;
        }
    }
}
