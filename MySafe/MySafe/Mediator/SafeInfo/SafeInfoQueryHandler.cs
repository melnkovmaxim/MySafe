using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.SafeInfo
{
    public class SafeInfoQueryHandler : IRequestHandler<SafeInfoQuery, SafeInfoResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public SafeInfoQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<SafeInfoResponse> Handle(SafeInfoQuery request, CancellationToken cancellationToken)
        {
            var cmdResponse = new SafeInfoResponse();

            try
            {
                _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);

                var httpRequest = new RestRequest("api/v1/my_safe", Method.GET);

                var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken)
                    .ConfigureAwait(false);
                
                if (!response.IsSuccessful)
                {
                    var errorResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
                    throw new HttpRequestException(errorResponse.Error);
                }

                cmdResponse = JsonConvert.DeserializeObject<SafeInfoResponse>(response.Content);
            }
            catch (Exception ex)
            {
                cmdResponse.Error = ex.Message;
            }

            return cmdResponse;
        }
    }
}
