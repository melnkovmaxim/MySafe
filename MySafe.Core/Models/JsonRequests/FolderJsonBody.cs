using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class FolderJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("name")] public string Name { get; set; }
    }
}