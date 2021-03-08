using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.DestroyTrashSheetCommand
{
    public class RemoveFileFromTrashCommandHandler: IRequestHandler<DestroyTrashSheetCommand, Sheet>
    {
        private readonly IRestClient _restClient;
        
        public RemoveFileFromTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Sheet> Handle(DestroyTrashSheetCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/sheets/{request.SheetId}", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<Sheet>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
