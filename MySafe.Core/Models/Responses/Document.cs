﻿using System;
using System.Collections.Generic;
using MySafe.Core.Entities.Responses.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Entities.Responses
{
    [JsonObject]
    public class Document : ResponseBase
    {
        [JsonProperty] public int Id { get; set; }

        [JsonProperty] public string Name { get; set; }

        [JsonProperty] public string Location { get; set; }

        [JsonProperty] public int FolderId { get; set; }

        [JsonProperty] public DateTime? CreatedAt { get; set; }

        [JsonProperty] public bool ConstainsAttachments { get; set; }

        [JsonProperty] public DateTime? TrashedAt { get; set; }

        [JsonProperty] public string Content { get; set; }

        [JsonProperty] public List<AttachmentResponse> Attachments { get; set; }
    }

    [JsonObject]
    public class AttachmentResponse : ResponseBase
    {
        [JsonProperty] public int Id { get; set; }

        [JsonProperty] public DateTime? CreatedAt { get; set; }

        [JsonProperty] public string Name { get; set; }

        [JsonProperty] public string Preview { get; set; }

        [JsonProperty] public int ViewWidth { get; set; }

        [JsonProperty] public int ViewHeight { get; set; }

        [JsonProperty("file_extension")] public string FileExtension { get; set; }

        [JsonProperty] public int PagesCount { get; set; }
    }
}