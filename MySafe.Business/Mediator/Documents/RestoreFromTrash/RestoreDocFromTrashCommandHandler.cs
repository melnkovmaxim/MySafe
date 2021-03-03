using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Documents.RemoveFromTrash;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.RestoreFromTrash
{
    public class RestoreDocFromTrashCommandHandler: IRequestHandler<RemoveDocFromTrashCommand, DocumentResponse>
    {
        private readonly IRestClient _restClient;
        
        public RestoreDocFromTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<DocumentResponse> Handle(RemoveDocFromTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/documents/{request.DocumentId}/restore", Method.PUT);
            var cmdResponse = await _restClient.GetResponseAsync<DocumentResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
