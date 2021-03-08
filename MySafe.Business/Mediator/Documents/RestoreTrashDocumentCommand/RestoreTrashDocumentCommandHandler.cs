using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.RestoreTrashDocumentCommand
{
    public class RestoreTrashDocumentCommandHandler: IRequestHandler<RestoreTrashDocumentCommand, Document>
    {
        private readonly IRestClient _restClient;
        
        public RestoreTrashDocumentCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Document> Handle(RestoreTrashDocumentCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken);
            var httpRequest = new RestRequest($"/api/v1/documents/{request.DocumentId}/restore", Method.PUT);
            var cmdResponse = await _restClient.GetResponseAsync<Document>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
