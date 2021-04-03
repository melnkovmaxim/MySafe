using System.Collections.Generic;
using MySafe.Core.Entities.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Models.Responses
{
    [JsonObject]
    public class SafeEntity : EntityBase, IEntity
    {
        [JsonProperty("capacity")] public double Capacity { get; set; }

        [JsonProperty("used_capacity")] public double UsedCapacity { get; set; }

        [JsonProperty("folders")] public List<FolderEntity> Folders { get; set; }
    }
}