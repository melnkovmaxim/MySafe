using AutoMapper;
using MediatR;
using MySafe.Presentation.Mediator.Documents.RemoveFromTrash;
using MySafe.Presentation.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Extensions;

namespace MySafe.Presentation.Mediator.Documents.RestoreFromTrash
{
    public class RestoreDocFromTrashCommandHandler: IRequestHandler<RemoveDocFromTrashCommand, DocumentResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public RestoreDocFromTrashCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
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
