using System;
using MySafe.Core.Entities.Responses.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Entities.Responses
{
    [JsonObject]
    public class Note : ResponseBase
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("created_at")] public DateTime? CreatedAt { get; set; }

        [JsonProperty("content")] public string Content { get; set; }

        [JsonProperty("clipped_content")] public string ClippedContent { get; set; }
    }
}