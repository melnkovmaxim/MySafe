﻿using MediatR;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MySafe.Business.Mediator.Trash.GetTrashInfo
{
    public class TrashInfoQueryHandler: IRequestHandler<TrashInfoQuery, List<TrashResponse>>
    {
        private readonly IRestClient _restClient;
        
        public TrashInfoQueryHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<List<TrashResponse>> Handle(TrashInfoQuery request, CancellationToken cancellationToken)
        {
            var httpRequest = new RestRequest("/api/v1/trash", Method.GET);
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken);
            var result = JsonConvert.DeserializeObject<List<TrashResponse>>(response.Content);

            return result;
        }
    }
}