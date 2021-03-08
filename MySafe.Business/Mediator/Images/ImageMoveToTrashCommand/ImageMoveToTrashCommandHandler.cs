using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;

namespace MySafe.Business.Mediator.Images.ImageMoveToTrashCommand
{
    public class ImageMoveToTrashCommandHandler: IRequestHandler<DestroyTrashImageCommand.DestroyTrashImageCommand, Image>
    {
        private readonly IRestClient _restClient;
        
        public ImageMoveToTrashCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Image> Handle(DestroyTrashImageCommand.DestroyTrashImageCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/images/{request.ImageId}/trash", Method.PUT);
            var cmdResponse = await _restClient.GetResponseAsync<Image>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
