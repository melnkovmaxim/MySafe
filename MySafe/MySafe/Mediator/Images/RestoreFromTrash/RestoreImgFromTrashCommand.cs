using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Images.RestoreFromTrash
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
