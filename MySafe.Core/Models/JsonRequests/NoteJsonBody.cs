using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class NoteJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("content")] public string Content { get; set; }
    }
}