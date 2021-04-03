using Newtonsoft.Json;

namespace MySafe.Core.Entities.Requests
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class User : JsonObjectBase
    {
        [JsonProperty("login")] public string Login { get; set; }

        [JsonProperty("password")] public string Password { get; set; }

        [JsonProperty("email")] public string Email { get; set; }
    }
}