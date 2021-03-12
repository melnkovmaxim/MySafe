using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Images.DestroyTrashImageCommand
{
    public class DestroyTrashImageCommand : BearerRequestBase<Image>
    {
        public int ImageId { get; set; } // attachment 

        public DestroyTrashImageCommand(int imageId)
        {
            ImageId = imageId;
        }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/images/{ImageId}";
    }
}
