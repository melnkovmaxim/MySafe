using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fody;
using MediatR;
using MySafe.Extensions;
using MySafe.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.SafeInfo
{
    [ConfigureAwait(false)]
    public class SafeInfoQueryHandler : IRequestHandler<SafeInfoQuery, SafeInfoResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public SafeInfoQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<SafeInfoResponse> Handle(SafeInfoQuery request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest("api/v1/my_safe", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<SafeInfoResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
