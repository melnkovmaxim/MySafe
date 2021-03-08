using MediatR;
using MySafe.Core.Entities.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Business.Mediator.Images.ImageMoveToTrashCommand
{
    public class ImageMoveToTrashCommand: IRequest<Image>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int ImageId { get; set; } // attachment 

        public ImageMoveToTrashCommand(JwtSecurityToken jwtToken, int imageId)
        {
            JwtToken = jwtToken;
            ImageId = imageId;
        }
    }
}
