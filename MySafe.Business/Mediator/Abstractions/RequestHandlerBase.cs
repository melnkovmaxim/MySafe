using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fody;
using MediatR;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Models;
using MySafe.Core.Models.JsonRequests;
using MySafe.Services.Extensions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Services.Mediator.Abstractions
{
    [ConfigureAwait(false)]
    public abstract class RequestHandlerBase<TRequest, TJsonBody, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : RequestBase<TResponse>
        where TJsonBody : IJsonBody
        where TResponse : IEntity
    {
        private readonly IMapper _mapper;
        private readonly IRestClient _restClient;

        protected RequestHandlerBase(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var httpRequest = new RestRequest(request.RequestResource, request.RequestMethod);

            if (typeof(TJsonBody) != typeof(EmptyJsonBody))
            {
                var jsonRequest = _mapper.Map<TJsonBody>(request);
                var json = jsonRequest as ISerializedObject != null
                    ? (jsonRequest as JsonObjectBase)?.SerializeWithRoot()
                    : JsonConvert.SerializeObject(jsonRequest);

                httpRequest.AddJsonBody(json);
            }

            if (request is BearerRequestBase<TResponse> bearerRequest && !string.IsNullOrEmpty(bearerRequest.JwtToken))
                _restClient.Authenticator = new JwtAuthenticator(bearerRequest.JwtToken);

            if (request is RequestUploadBase<TResponse> uploadRequest)
            {
                httpRequest.AddFile("file", uploadRequest.FileBytes, uploadRequest.FileName, uploadRequest.ContentType);
                httpRequest.AlwaysMultipartFormData = true;
            }

            cancellationToken.ThrowIfCancellationRequested();

            return await _restClient.SendAndGetResponseAsync<TResponse>(httpRequest, cancellationToken);
        }
    }
}