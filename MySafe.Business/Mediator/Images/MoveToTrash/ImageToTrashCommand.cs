using MediatR;
using MySafe.Core.Entities.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Business.Mediator.Images.MoveToTrash
{
    public class ImageToTrashCommand: IRequest<ImageResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int ImageId { get; set; } // attachment 

        public ImageToTrashCommand(JwtSecurityToken jwtToken, int imageId)
        {
            JwtToken = jwtToken;
            ImageId = imageId;
        }
    }
}
