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

namespace MySafe.Mediator.Safe.SafeInfo
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
