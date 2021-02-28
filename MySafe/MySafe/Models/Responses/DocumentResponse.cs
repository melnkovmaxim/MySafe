using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

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

        [JsonProperty("file_extension")]
        public string FileExtension { get; set; }

        [JsonProperty]
        public int PagesCount { get; set; }

        [JsonIgnore] 
        public bool IsImage => FileExtension == null;

        [JsonIgnore]
        public ImageSource ImageSource => _imageSource ??= _imageSourceLazy.Value;

        [JsonIgnore]
        private ImageSource _imageSource;

        [JsonIgnore] 
        private Lazy<ImageSource> _imageSourceLazy => new Lazy<ImageSource>(() => 
        {
            var reg = new Regex(".*base64,");
            var base64 = reg.Replace(Preview, "").Replace("\\n", "");
            var @byte = Convert.FromBase64String(base64);
            return ImageSource.FromStream(() => new MemoryStream(@byte));
        });
    }
}
