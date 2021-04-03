using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.DestroyTrashImageCommand
{
    public class DestroyTrashImageCommand : BearerRequestBase<ImageEntity>
    {
        public DestroyTrashImageCommand(int imageId)
        {
            ImageId = imageId;
        }

        public int ImageId { get; set; } // attachment 

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/images/{ImageId}";
    }
}