using MediatR;
using MySafe.Core.Entities.Responses;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Business.Mediator.Images.OriginalImageQuery
{
    /// <summary>
    /// Получить оригинальное изображение
    /// </summary>
    public class OriginalImageQuery: BearerRequestBase<Image>
    {
        public int ImageId { get; set; }

        public OriginalImageQuery(string jwtToken, int imageId) : base(jwtToken)
        {
            ImageId = imageId;
        }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"/api/v1/images/{ImageId}";
    }
}
