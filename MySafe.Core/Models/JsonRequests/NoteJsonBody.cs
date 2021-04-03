using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class NoteJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("content")] public string Content { get; set; }
    }
}