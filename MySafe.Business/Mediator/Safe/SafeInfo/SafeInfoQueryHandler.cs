﻿using Fody;
using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;

namespace MySafe.Business.Mediator.Safe.SafeInfo
{
    [ConfigureAwait(false)]
    public class SafeInfoQueryHandler : IRequestHandler<SafeInfoQuery, SafeResponse>
    {
        private readonly IRestClient _restClient;
        
        public SafeInfoQueryHandler(IRestClient restClient)
        {
            _restClient = restClient;
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