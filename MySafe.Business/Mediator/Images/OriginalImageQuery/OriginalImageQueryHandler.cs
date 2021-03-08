using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Images.OriginalImageQuery
{
    public class OriginalImageQueryHandler: IRequestHandler<OriginalImageQuery, Image>
    {
        private readonly IRestClient _restClient;
        
        public OriginalImageQueryHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Image> Handle(OriginalImageQuery request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/images/{request.ImageId}", Method.GET);
            var cmdResponse = await _restClient.GetResponseAsync<Image>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
