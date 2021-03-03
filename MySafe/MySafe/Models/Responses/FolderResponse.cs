using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Presentation.Models.Responses.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Presentation.Models.Responses
{
    [JsonObject]
    public class FolderResponse: BaseResponse
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public List<DocumentResponse> Documents { get; set; }
    }
}
