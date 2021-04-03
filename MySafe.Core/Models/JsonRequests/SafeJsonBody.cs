using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class SafeJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("current_password")] public string CurrentPassword { get; set; }
    }
}