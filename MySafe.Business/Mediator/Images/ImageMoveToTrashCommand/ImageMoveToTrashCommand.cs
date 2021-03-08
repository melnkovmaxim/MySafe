using MediatR;
using MySafe.Core.Entities.Responses;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Business.Mediator.Images.ImageMoveToTrashCommand
{
    public class ImageMoveToTrashCommand: BearerRequestBase<Image>
    {
        public int ImageId { get; set; } // attachment 

        public ImageMoveToTrashCommand(string jwtToken, int imageId) : base(jwtToken)
        {
            ImageId = imageId;
        }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/images/{ImageId}/trash";
    }
}
