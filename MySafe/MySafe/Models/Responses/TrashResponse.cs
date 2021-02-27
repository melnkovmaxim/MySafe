using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonArray]
    public class TrashResponse: BaseResponse
    {
        public DocumentResponse Document { get; set; }
        public FolderResponse Folder { get; set; }
    }
}
