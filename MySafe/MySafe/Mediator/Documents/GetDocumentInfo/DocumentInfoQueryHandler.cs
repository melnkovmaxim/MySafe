using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Extensions;
using MySafe.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.Documents.GetDocumentInfo
{
    public class DocumentInfoQueryHandler : IRequestHandler<DocumentInfoQuery, DocumentResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public DocumentInfoQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
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
