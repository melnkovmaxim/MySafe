using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Images.DestroyTrashImageCommand
{
    public class DestroyTrashImageCommandHandler: IRequestHandler<DestroyTrashImageCommand, Image>
    {
        private readonly IRestClient _restClient;
        
        public DestroyTrashImageCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Image> Handle(DestroyTrashImageCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/images/{request.ImageId}", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<Image>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
