using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class SafeJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("current_password")] public string CurrentPassword { get; set; }
    }
}