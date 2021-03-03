using AutoMapper;
using Fody;
using MediatR;
using MySafe.Presentation.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Extensions;

namespace MySafe.Presentation.Mediator.Safe.SafeInfo
{
    [ConfigureAwait(false)]
    public class SafeInfoQueryHandler : IRequestHandler<SafeInfoQuery, SafeResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public SafeInfoQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<SafeResponse> Handle(SafeInfoQuery request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest("api/v1/my_safe", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<SafeResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
