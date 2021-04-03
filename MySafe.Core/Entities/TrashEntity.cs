using System;
using MySafe.Core.Entities;
using MySafe.Core.Entities.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Models.Responses
{
    public class TrashEntity : AttachmentEntity, IEntity
    {
        [JsonProperty] public string Location { get; set; }

        [JsonProperty("folder_id")] public int? FolderId { get; set; }

        [JsonProperty] public bool ConstainsAttachments { get; set; }

        [JsonProperty] public DateTime? TrashedAt { get; set; }

        [JsonProperty] public string Content { get; set; }

        [JsonIgnore] public bool IsFolder => FolderId != null;

        [JsonIgnore] public bool IsDocument => FolderId == null;
    }
}