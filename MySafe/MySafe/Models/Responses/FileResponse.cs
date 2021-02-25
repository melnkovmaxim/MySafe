using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class FileResponse: BaseResponse
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public List<Attachment> Attachments { get; set; }
    }
    
    [JsonObject]
    public class Attachment
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Preview { get; set; }
    }
}
