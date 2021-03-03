using MediatR;
using MySafe.Presentation.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Images.RestoreFromTrash
{
    public class RestoreImgFromTrashCommand: IRequest<ImageResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int ImageId { get; set; } // attachment 

        public RestoreImgFromTrashCommand(JwtSecurityToken jwtToken, int imageId)
        {
            JwtToken = jwtToken;
            ImageId = imageId;
        }
    }
}
