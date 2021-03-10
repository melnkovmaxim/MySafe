using MediatR;
using MySafe.Core.Entities.Responses;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Business.Mediator.Images.RestoreTrashImageCommand
{
    /// <summary>
    /// Восстановить изображение из корзины
    /// </summary>
    public class RestoreTrashImageCommand: BearerRequestBase<Image>
    {
        public int ImageId { get; set; }

        public RestoreTrashImageCommand(int imageId)
        {
            ImageId = imageId;
        }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/images/{ImageId}/restore";
    }
}
