﻿using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class DocumentJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("folder_id")] public int FolderId { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("location")] public string Location { get; set; }
    }
}