using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.RestoreTrashImageCommand
{
    /// <summary>
    ///     Восстановить изображение из корзины
    /// </summary>
    public class RestoreTrashImageCommand : BearerRequestBase<Image>
    {
        public RestoreTrashImageCommand(int imageId)
        {
            ImageId = imageId;
        }

        public int ImageId { get; set; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/images/{ImageId}/restore";
    }
}