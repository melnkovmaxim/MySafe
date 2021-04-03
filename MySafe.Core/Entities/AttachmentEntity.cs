using System;
using MySafe.Core.Entities.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Entities
{
    public class AttachmentEntity : EntityBase, IEntity
    {
        [JsonProperty] public int Id { get; set; }

        [JsonProperty] public DateTime? CreatedAt { get; set; }

        [JsonProperty] public string Name { get; set; }

        [JsonProperty] public string Preview { get; set; }

        [JsonProperty] public int ViewWidth { get; set; }

        [JsonProperty] public int ViewHeight { get; set; }

        [JsonProperty("file_extension")] public string FileExtension { get; set; }

        [JsonProperty] public int PagesCount { get; set; }
    }
}