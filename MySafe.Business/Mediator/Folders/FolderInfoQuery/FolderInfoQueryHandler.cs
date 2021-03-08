﻿using Fody;
using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Folders.FolderInfoQuery
{
    [ConfigureAwait(false)]
    public class FolderInfoQueryHandler : IRequestHandler<FolderInfoQuery, Folder>
    {
        private readonly IRestClient _restClient;
        
        public FolderInfoQueryHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Folder> Handle(FolderInfoQuery request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken);
            var httpRequest = new RestRequest($"api/v1/folders/{request.DocumentId}", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<Folder>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
