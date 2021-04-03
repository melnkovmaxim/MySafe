using System.Collections.Generic;
using MySafe.Core.Entities.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Models.Responses
{
    [JsonObject]
    public class FolderEntity : EntityBase, IEntity
    {
        [JsonProperty] public int Id { get; set; }

        [JsonProperty] public string Name { get; set; }

        [JsonProperty] public List<DocumentEntity> Documents { get; set; }
    }
}