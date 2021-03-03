using AutoMapper;
using MediatR;
using MySafe.Presentation.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Extensions;

namespace MySafe.Presentation.Mediator.Documents.CreateDocument
{
    public class CreateDocumentCommandHandler: IRequestHandler<CreateDocumentCommand, DocumentResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public CreateDocumentCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
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
