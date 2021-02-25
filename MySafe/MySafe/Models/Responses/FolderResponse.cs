using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class FolderResponse: BaseResponse
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public List<Document> Documents { get; set; }
    }
    
    [JsonObject]
    public class Document
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Location { get; set; }
        [JsonProperty]
        public int FolderId { get; set; }
        [JsonProperty]
        public string CreatedAt { get; set; }
        [JsonProperty]
        public bool ContainsAttachments { get; set; }
    }
}
