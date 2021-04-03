using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Images.ChangeImageCommand
{
    /// <summary>
    ///     Изменить изображение
    /// </summary>
    public class ChangeImageCommand : BearerRequestBase<Image>
    {
        public ChangeImageCommand(int imageId, string rotate)
        {
            ImageId = imageId;
            Rotate = rotate;
        }

        [JsonIgnore] public int ImageId { get; }

        [JsonProperty("rotate")] public string Rotate { get; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/images/{ImageId}";
    }
}