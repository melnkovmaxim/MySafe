using System;
using MySafe.Core.Entities.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Models.Responses
{
    [JsonObject]
    public class NoteEntity : EntityBase, IEntity
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("created_at")] public DateTime? CreatedAt { get; set; }

        [JsonProperty("content")] public string Content { get; set; }

        [JsonProperty("clipped_content")] public string ClippedContent { get; set; }
    }
}