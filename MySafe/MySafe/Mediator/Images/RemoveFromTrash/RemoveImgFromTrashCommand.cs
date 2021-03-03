using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Presentation.Models.Responses;

namespace MySafe.Presentation.Mediator.Images.RemoveFromTrash
{
    public class RemoveImgFromTrashCommand: IRequest<ImageResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int ImageId { get; set; } // attachment 

        public RemoveImgFromTrashCommand(JwtSecurityToken jwtToken, int imageId)
        {
            JwtToken = jwtToken;
            ImageId = imageId;
        }
    }
}
