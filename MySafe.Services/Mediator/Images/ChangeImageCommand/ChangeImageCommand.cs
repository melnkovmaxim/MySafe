using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.ChangeImageCommand
{
    /// <summary>
    ///     Изменить изображение
    /// </summary>
    public class ChangeImageCommand : BearerRequestBase<ImageEntity>
    {
        public ChangeImageCommand(int imageId, string rotate)
        {
            ImageId = imageId;
            Rotate = rotate;
        }

        public int ImageId { get; }
        public string Rotate { get; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/images/{ImageId}";
    }
}