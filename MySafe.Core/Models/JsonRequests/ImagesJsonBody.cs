using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class ImagesJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("rotate")] public string Rotate { get; set; }
    }
}