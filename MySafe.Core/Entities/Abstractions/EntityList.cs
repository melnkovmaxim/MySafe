using System.Collections.Generic;
using Newtonsoft.Json;

namespace MySafe.Core.Entities.Abstractions
{
    public class EntityList<TEntity> : List<TEntity>, IEntity
        where TEntity : IEntity
    {
        [JsonProperty("error")] public string Error { get; set; }

        public bool HasError { get; }
        public byte[] FileBytes { get; set; }
    }
}