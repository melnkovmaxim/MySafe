using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.DocumentInfoQuery
{
    public class DocumentInfoQueryHandler : IRequestHandler<DocumentInfoQuery, Document>
    {
        private readonly IRestClient _restClient;
        
        public DocumentInfoQueryHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Document> Handle(DocumentInfoQuery request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken);
            var httpRequest = new RestRequest($"api/v1/documents/{request.FileId}", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<Document>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
