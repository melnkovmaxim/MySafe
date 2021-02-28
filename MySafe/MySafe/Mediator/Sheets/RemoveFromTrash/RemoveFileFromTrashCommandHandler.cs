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

namespace MySafe.Mediator.Sheets.RemoveFromTrash
{
    public class RemoveFileFromTrashCommandHandler: IRequestHandler<RemoveFileFromTrashCommand, SheetResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public RemoveFileFromTrashCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<SheetResponse> Handle(RemoveFileFromTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/sheets/{request.SheetId}", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<SheetResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
