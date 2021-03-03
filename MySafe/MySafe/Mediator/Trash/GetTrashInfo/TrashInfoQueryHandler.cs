using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Models.Responses;

namespace MySafe.Presentation.Mediator.Trash.GetTrashInfo
{
    public class TrashInfoQueryHandler: IRequestHandler<TrashInfoQuery, List<TrashResponse>>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public TrashInfoQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
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
