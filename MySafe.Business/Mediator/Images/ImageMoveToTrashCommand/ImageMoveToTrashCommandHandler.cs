using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.ImageMoveToTrashCommand
{
    public class ImageMoveToTrashCommandHandler : RequestHandlerBase<ImageMoveToTrashCommand, Image>
    {
        public ImageMoveToTrashCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}