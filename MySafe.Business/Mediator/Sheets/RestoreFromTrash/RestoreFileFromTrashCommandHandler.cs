using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.RestoreFromTrash
{
    public class RestoreFileFromTrashCommandHandler: IRequestHandler<RestoreFileFromTrashCommand, SheetResponse>
    {
        private readonly IRestClient _restClient;
        
        public RestoreFileFromTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<SheetResponse> Handle(RestoreFileFromTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/sheets/{request.SheetId}", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<SheetResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
