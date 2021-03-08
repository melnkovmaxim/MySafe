using MediatR;
using MySafe.Core.Entities.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Business.Mediator.Images.RestoreTrashImageCommand
{
    /// <summary>
    /// Восстановить изображение из корзины
    /// </summary>
    public class RestoreTrashImageCommand: IRequest<Image>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int ImageId { get; set; } // attachment 

        public RestoreTrashImageCommand(JwtSecurityToken jwtToken, int imageId)
        {
            JwtToken = jwtToken;
            ImageId = imageId;
        }
    }
}
