using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;

namespace MySafe.Business.Mediator.Sheets.MoveToTrash
{
    public class MoveFileToTrashCommandHandler: IRequestHandler<MoveFileToTrashCommand, SheetResponse>
    {
        private readonly IRestClient _restClient;
        
        public MoveFileToTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<SheetResponse> Handle(MoveFileToTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/sheets/{request.SheetId}/trash", Method.PUT);
            var cmdResponse = await _restClient.GetResponseAsync<SheetResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
