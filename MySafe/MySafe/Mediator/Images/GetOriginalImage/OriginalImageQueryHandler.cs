using AutoMapper;
using MediatR;
using MySafe.Presentation.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Extensions;

namespace MySafe.Presentation.Mediator.Images.GetOriginalImage
{
    public class OriginalImageQueryHandler: IRequestHandler<OriginalImageQuery, ImageResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public OriginalImageQueryHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<ImageResponse> Handle(OriginalImageQuery request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/images/{request.ImageId}", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<ImageResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
