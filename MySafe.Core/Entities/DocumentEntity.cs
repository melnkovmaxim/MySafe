using System;
using System.Collections.Generic;
using MySafe.Core.Entities;
using MySafe.Core.Entities.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Models.Responses
{
    [JsonObject]
    public class DocumentEntity : EntityBase, IEntity
    {
        [JsonProperty] public int Id { get; set; }

        [JsonProperty] public string Name { get; set; }

        [JsonProperty] public string Location { get; set; }

        [JsonProperty("folder_id")] public int FolderId { get; set; }

        [JsonProperty] public DateTime? CreatedAt { get; set; }

        [JsonProperty] public bool ConstainsAttachments { get; set; }

        [JsonProperty] public DateTime? TrashedAt { get; set; }

        [JsonProperty] public string Content { get; set; }

        [JsonProperty] public List<AttachmentEntity> Attachments { get; set; }
    }
}