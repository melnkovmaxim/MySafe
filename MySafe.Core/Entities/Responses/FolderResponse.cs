using Newtonsoft.Json;
using System.Collections.Generic;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Core.Entities.Responses
{
    [JsonObject]
    public class FolderResponse: BaseResponse
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public List<DocumentResponse> Documents { get; set; }
    }
}
