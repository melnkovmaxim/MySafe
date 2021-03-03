using MediatR;
using MySafe.Core.Entities.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Business.Mediator.Images.GetOriginalImage
{
    public class OriginalImageQuery: IRequest<ImageResponse>
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
