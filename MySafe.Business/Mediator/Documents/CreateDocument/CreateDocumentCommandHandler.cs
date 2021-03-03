using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;

namespace MySafe.Business.Mediator.Documents.CreateDocument
{
    public class CreateDocumentCommandHandler: IRequestHandler<CreateDocumentCommand, DocumentResponse>
    {
        private readonly IRestClient _restClient;
        
        public CreateDocumentCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<DocumentResponse> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/folders/{request.FolderId}/documents", Method.POST);
            var cmdResponse = await _restClient.GetResponseAsync<DocumentResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
