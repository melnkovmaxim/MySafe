using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;

namespace MySafe.Business.Mediator.Sheets.SheetMoveToTrashCommand
{
    public class MoveFileToTrashCommandHandler: IRequestHandler<SheetMoveToTrashCommand, Sheet>
    {
        private readonly IRestClient _restClient;
        
        public MoveFileToTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Sheet> Handle(SheetMoveToTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/sheets/{request.SheetId}/trash", Method.PUT);
            var cmdResponse = await _restClient.GetResponseAsync<Sheet>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
