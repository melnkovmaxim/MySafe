using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;

namespace MySafe.Business.Mediator.Sheets.GetFile
{
    public class FileQueryHandler: IRequestHandler<FileQuery, SheetResponse>
    {
        private readonly IRestClient _restClient;
        
        public FileQueryHandler(IRestClient restClient)
        {
            _restClient = restClient;
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
