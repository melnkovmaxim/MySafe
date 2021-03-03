using Newtonsoft.Json;

namespace MySafe.Presentation.Models.Requests
{
    [JsonObject]
    public class TwoFactor : JsonObjectBase
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
