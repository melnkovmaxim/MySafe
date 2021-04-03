using Newtonsoft.Json;

namespace MySafe.Core.Entities.Requests
{
    [JsonObject]
    public class TwoFactor : JsonObjectBase
    {
        [JsonProperty("code")] public string Code { get; set; }
    }
}