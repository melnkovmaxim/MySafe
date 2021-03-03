using AutoMapper;
using Fody;
using MediatR;
using MySafe.Presentation.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Extensions;

namespace MySafe.Presentation.Mediator.Folders.GetFolderInfo
{
    [ConfigureAwait(false)]
    public class FolderInfoQueryHandler : IRequestHandler<FolderInfoQuery, FolderResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public FolderInfoQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<FolderResponse> Handle(FolderInfoQuery request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"api/v1/folders/{request.DocumentId}", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<FolderResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
