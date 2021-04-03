using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class ImagesJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("rotate")] public string Rotate { get; set; }
    }
}