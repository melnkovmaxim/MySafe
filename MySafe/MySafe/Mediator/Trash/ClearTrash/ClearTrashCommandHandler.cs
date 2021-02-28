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

namespace MySafe.Mediator.Trash.ClearTrash
{
    public class ClearTrashCommandHandler: IRequestHandler<ClearTrashCommand, BaseResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public ClearTrashCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(ClearTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest("/api/v1/trash", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<BaseResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
