using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.RemoveFromTrash
{
    public class RemoveDocFromTrashCommandHandler: IRequestHandler<RemoveDocFromTrashCommand, DocumentResponse>
    {
        private readonly IRestClient _restClient;
        
        public RemoveDocFromTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<DocumentResponse> Handle(RemoveDocFromTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/documents/{request.DocumentId}", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<DocumentResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
