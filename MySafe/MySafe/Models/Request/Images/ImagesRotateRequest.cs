using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models.Request.Images
{
    [JsonObject]
    public class ImagesRotateRequest : IToken
    {
        public string AccessToken { get; set; }

        [JsonProperty("rotate")]
        public char Rotate { get; set; }
    }
}
