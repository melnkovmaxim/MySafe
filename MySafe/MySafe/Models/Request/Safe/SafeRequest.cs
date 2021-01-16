using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models.Request.Safe
{
    [JsonObject]
    public class SafeRequest : IToken
    {
        [JsonIgnore]
        public string AccessToken { get; set; }

        [JsonProperty("current_password")]
        public string Password { get; set; }
    }
}
