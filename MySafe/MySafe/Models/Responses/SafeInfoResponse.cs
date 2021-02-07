using System.Collections.Generic;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class SafeInfoResponse : BaseResponse
    {
        [JsonProperty("capacity")]
        public double Capacity { get; set; }

        [JsonProperty("used_capacity")]
        public double UsedCapacity { get; set; }

        [JsonProperty("folders")]
        public List<SafeFolder> Folders { get; set; }
    }

    [JsonObject]
    public class SafeFolder
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
