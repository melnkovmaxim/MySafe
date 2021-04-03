using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.ImageMoveToTrashCommand
{
    public class ImageMoveToTrashCommand : BearerRequestBase<Image>
    {
        public ImageMoveToTrashCommand(int imageId)
        {
            ImageId = imageId;
        }

        public int ImageId { get; set; } // attachment 

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/images/{ImageId}/trash";
    }
}