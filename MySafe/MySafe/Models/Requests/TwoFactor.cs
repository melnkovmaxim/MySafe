using Newtonsoft.Json;

namespace MySafe.Models.Requests
{
    [JsonObject]
    public class TwoFactor : BaseJsonObject
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
