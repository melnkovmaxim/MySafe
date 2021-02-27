using System.Collections.Generic;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class SafeResponse : BaseResponse
    {
        [JsonProperty("capacity")]
        public double Capacity { get; set; }

        [JsonProperty("used_capacity")]
        public double UsedCapacity { get; set; }

        [JsonProperty("folders")]
        public List<FolderResponse> Folders { get; set; }
    }
}
