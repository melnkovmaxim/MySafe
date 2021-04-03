using System.Threading;
using System.Threading.Tasks;
using Fody;
using MediatR;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Services.Extensions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Services.Mediator.Abstractions
{
    [ConfigureAwait(false)]
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : RequestBase<TResponse>
        where TResponse : IResponse
    {
        private readonly IRestClient _restClient;

        protected RequestHandlerBase(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpRequest = new RestRequest(request.RequestResource, request.RequestMethod)
                .AddJsonBody(json);

            if (request is BearerRequestBase<TResponse> bearerRequest && !string.IsNullOrEmpty(bearerRequest.JwtToken))
                _restClient.Authenticator = new JwtAuthenticator(bearerRequest.JwtToken);

            if (request is RequestUploadBase<TResponse> uploadRequest)
            {
                httpRequest.AddFile("file", uploadRequest.FileBytes, uploadRequest.FileName, uploadRequest.ContentType);
                httpRequest.AlwaysMultipartFormData = true;
                //httpRequest.AddHeader("Content-Length", uploadRequest.FileBytes.Length.ToString());
            }

            cancellationToken.ThrowIfCancellationRequested();

            return _restClient.SendAndGetResponseAsync<TResponse>(httpRequest, cancellationToken);
        }
    }
}