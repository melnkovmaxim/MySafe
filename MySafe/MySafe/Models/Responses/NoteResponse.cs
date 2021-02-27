using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class NoteResponse: BaseResponse
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty]
        public string Content { get; set; }

        [JsonProperty]
        public string ClippedContent { get; set; }
    }
}
