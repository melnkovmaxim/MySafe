using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Business.Mediator.Abstractions
{
    public abstract class AuthorizedRequestHandlerBase<TRequest, TResponse>: IRequestHandler<AuthorizedRequestBase<TResponse>, TResponse>
        where TRequest : AuthorizedRequestBase<TResponse>
        where TResponse: IResponse
    {
        private readonly IRestClient _restClient;

        protected AuthorizedRequestHandlerBase(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<TResponse> Handle(AuthorizedRequestBase<TResponse> request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken);
            var httpRequest = new RestRequest(request.RequestResource, request.RequestMethod);
            var cmdResponse = await _restClient.GetResponseAsync<TResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
