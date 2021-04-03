using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class FolderJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("name")] public string Name { get; set; }
    }
}