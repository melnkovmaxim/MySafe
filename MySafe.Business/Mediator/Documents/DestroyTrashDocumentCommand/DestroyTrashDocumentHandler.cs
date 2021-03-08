using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.DestroyTrashDocumentCommand
{
    public class DestroyInTrashCommandHandler: IRequestHandler<DestroyTrashDocumentCommand, DocumentResponse>
    {
        private readonly IRestClient _restClient;
        
        public DestroyInTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<DocumentResponse> Handle(DestroyTrashDocumentCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken);
            var httpRequest = new RestRequest($"/api/v1/documents/{request.DocumentId}", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<DocumentResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
