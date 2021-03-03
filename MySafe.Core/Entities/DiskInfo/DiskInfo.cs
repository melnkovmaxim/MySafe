using MySafe.Core.Commands;
using Newtonsoft.Json;

namespace MySafe.Core.Entities.DiskInfo
{
    [JsonObject]
    public class DiskInfo : BindableBase
    {
        [JsonProperty("max_file_size")]
        public long MaxFileSize { get; set; }

        [JsonProperty("unlimited_autoupload_enabled")]
        public bool Unlimited { get; set; }

        [JsonProperty("total_space")]
        public long TotalSpace { get; set; }

        [JsonProperty("trash_size")]
        public long TrashSize { get; set; }

        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }

        [JsonProperty("used_space")]
        public long UsedSpace { get; set; }

        [JsonProperty("user")]
        public UserInfo User { get; set; }
    }
}
