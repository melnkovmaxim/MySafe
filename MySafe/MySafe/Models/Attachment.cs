using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySafe.Core.Enums;
using Xamarin.Forms;

namespace MySafe.Presentation.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileExtension { get; set; }
        public string Name { get; set; }
        public bool IsImage => FileExtension == null;
        public string Preview { get; set; }

        public ImageSource ImageSource
        {
            get
            {
                if (string.IsNullOrEmpty(Preview)) return null;
                return _imageSource ??= _imageSourceLazy.Value;
            }
        }

        public string Base64 => new Regex(".*base64,").Replace(Preview, "").Replace("\\n", "");
        public AttachmentTypeEnum Type => IsImage ? AttachmentTypeEnum.Image : AttachmentTypeEnum.Other;

        private ImageSource _imageSource;

        private Lazy<ImageSource> _imageSourceLazy => new Lazy<ImageSource>(() =>
        {
            var @byte = Convert.FromBase64String(Base64);
            return ImageSource.FromStream(() => new MemoryStream(@byte));
        });
    }
}
