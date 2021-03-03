using MySafe.Core.Entities.Responses.Abstractions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MySafe.Core.Entities.Responses
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
