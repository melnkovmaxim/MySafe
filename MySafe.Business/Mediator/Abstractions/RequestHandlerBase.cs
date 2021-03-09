using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fody;
using MediatR;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization;

namespace MySafe.Business.Mediator.Abstractions
{
    [ConfigureAwait(false)]
    public abstract class RequestHandlerBase<TRequest, TResponse>: IRequestHandler<TRequest, TResponse>
        where TRequest : RequestBase<TResponse>
        where TResponse: IResponse
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;

        protected RequestHandlerBase(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request is BearerRequestBase<TResponse> bearerRequest && !string.IsNullOrEmpty(bearerRequest.JwtToken))
            {
                _restClient.Authenticator = new JwtAuthenticator(bearerRequest.JwtToken);
            }

            var json = JsonConvert.SerializeObject(request);
            var httpRequest = new RestRequest(request.RequestResource, request.RequestMethod)
                .AddJsonBody(json);

            if (request is RequestUploadBase<TResponse> uploadRequest)
            {
                httpRequest.AddFile("file", uploadRequest.FileBytes, uploadRequest.FileName, uploadRequest.ContentType);
                httpRequest.AlwaysMultipartFormData = true;
            }

            return _restClient.SendAndGetResponseAsync<TResponse>(httpRequest, cancellationToken);
        }
    }
}
