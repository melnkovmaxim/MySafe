using MediatR;
using MySafe.Core.Entities.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Business.Mediator.Images.OriginalImageQuery
{
    /// <summary>
    /// Получить оригинальное изображение
    /// </summary>
    public class OriginalImageQuery: IRequest<Image>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int ImageId { get; set; } // attachment 

        public OriginalImageQuery(JwtSecurityToken jwtToken, int imageId)
        {
            JwtToken = jwtToken;
            ImageId = imageId;
        }
    }
}
