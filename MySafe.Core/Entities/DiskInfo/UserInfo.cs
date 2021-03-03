using MySafe.Core.Commands;
using Newtonsoft.Json;

namespace MySafe.Core.Entities.DiskInfo
{
    [JsonObject]
    public class UserInfo : BindableBase
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("display_name")]
        public string Name { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }
    }
}
