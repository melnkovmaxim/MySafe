﻿using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.GetDocumentInfo
{
    public class DocumentInfoQueryHandler : IRequestHandler<DocumentInfoQuery, DocumentResponse>
    {
        private readonly IRestClient _restClient;
        
        public DocumentInfoQueryHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<DocumentResponse> Handle(DocumentInfoQuery request, CancellationToken cancellationToken)
        {
            
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"api/v1/documents/{request.FileId}", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<DocumentResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}