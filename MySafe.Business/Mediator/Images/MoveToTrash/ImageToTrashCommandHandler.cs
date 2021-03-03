using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;

namespace MySafe.Business.Mediator.Images.MoveToTrash
{
    public class ImageToTrashCommandHandler: IRequestHandler<ImageToTrashCommand, ImageResponse>
    {
        private readonly IRestClient _restClient;
        
        public ImageToTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ImageResponse> Handle(ImageToTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/images/{request.ImageId}/trash", Method.PUT);
            var cmdResponse = await _restClient.GetResponseAsync<ImageResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
