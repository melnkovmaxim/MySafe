using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Images.ChangeImageCommand
{
    /// <summary>
    /// Изменить изображение
    /// </summary>
    public class ChangeImageCommand:  BearerRequestBase<Image>
    {
        [JsonIgnore]
        public int ImageId { get; }

        [JsonProperty("rotate")]
        public string Rotate { get; }

        public ChangeImageCommand(int imageId, string rotate)
        {
            ImageId = imageId;
            Rotate = rotate;
        }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/images/{ImageId}";
    }
}
