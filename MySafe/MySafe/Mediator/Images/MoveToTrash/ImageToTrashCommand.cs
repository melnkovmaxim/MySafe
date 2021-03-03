using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Presentation.Models.Responses;

namespace MySafe.Presentation.Mediator.Images.MoveToTrash
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
