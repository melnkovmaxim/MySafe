using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class BaseResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        public bool HasError => !string.IsNullOrEmpty(Error);
    }
}
