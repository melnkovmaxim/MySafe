using Newtonsoft.Json;

namespace MySafe.Core.Entities.Responses.Abstractions
{
    [JsonObject]
    public class ResponseBase : IResponse
    {
        [JsonProperty("error")] public string Error { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Error);
        public byte[] FileBytes { get; set; }
    }
}