using Newtonsoft.Json;

namespace MySafe.Core.Entities.Abstractions
{
    public class EntityBase : IEntity
    {
        [JsonProperty("error")] public string Error { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Error);
        public byte[] FileBytes { get; set; }
    }
}