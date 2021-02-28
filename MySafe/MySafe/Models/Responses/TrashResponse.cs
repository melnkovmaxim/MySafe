using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class TrashResponse: Attachment
    {
        [JsonProperty]
        public string Location { get; set; }

        [JsonProperty("folder_id")]
        public int? FolderId { get; set; }

        [JsonProperty]
        public bool ConstainsAttachments { get; set; }

        [JsonProperty]
        public DateTime? TrashedAt { get; set; }

        [JsonProperty]
        public string Content { get; set; }

        [JsonIgnore] 
        public bool IsFolder => FolderId != null;

        [JsonIgnore] 
        public bool IsDocument => FolderId == null;
    }
}
