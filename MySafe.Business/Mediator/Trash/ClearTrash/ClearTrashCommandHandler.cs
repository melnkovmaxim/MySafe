using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Business.Mediator.Trash.ClearTrash
{
    public class ClearTrashCommandHandler: IRequestHandler<ClearTrashCommand, BaseResponse>
    {
        private readonly IRestClient _restClient;
        
        public ClearTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<BaseResponse> Handle(ClearTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest("/api/v1/trash", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<BaseResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
