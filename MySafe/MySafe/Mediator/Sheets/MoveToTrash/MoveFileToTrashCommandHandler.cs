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

namespace MySafe.Mediator.Sheets.MoveToTrash
{
    public class MoveFileToTrashCommandHandler: IRequestHandler<MoveFileToTrashCommand, SheetResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public MoveFileToTrashCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<SheetResponse> Handle(MoveFileToTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/sheets/{request.SheetId}/trash", Method.PUT);
            var cmdResponse = await _restClient.GetResponseAsync<SheetResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
