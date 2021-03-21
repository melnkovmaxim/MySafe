﻿using MySafe.Core.Entities.Responses.Abstractions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MySafe.Core.Entities.Responses
{
    [JsonObject]
    public class Safe : ResponseBase
    {
        [JsonProperty("capacity")]
        public double Capacity { get; set; }

        [JsonProperty("used_capacity")]
        public double UsedCapacity { get; set; }

        [JsonProperty("folders")]
        public List<Folder> Folders { get; set; }
    }
}