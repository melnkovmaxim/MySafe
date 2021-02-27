using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class DocumentResponse: BaseResponse
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
        public DateTime? CreatedAt { get; set; }

        [JsonProperty]
        public bool ConstainsAttachments { get; set; }

        [JsonProperty]
        public DateTime? TrashedAt { get; set; }

        [JsonProperty]
        // Возможно неверный тип
        public string Content { get; set; }

        [JsonProperty]
        public List<Attachment> Attachments { get; set; }
    }
    
    [JsonObject]
    public class Attachment
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Preview { get; set; }
        
        [JsonProperty]
        public int ViewWidth { get; set; }

        [JsonProperty]
        public int ViewHeight { get; set; }

        [JsonProperty]
        public string FileExtensions { get; set; }

        [JsonProperty]
        public int PagesCount { get; set; }
    }
}
