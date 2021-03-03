using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        private ImageSource _imageSource;

        private Lazy<ImageSource> _imageSourceLazy => new Lazy<ImageSource>(() =>
        {
            var reg = new Regex(".*base64,");
            var base64 = reg.Replace(Preview, "").Replace("\\n", "");
            var @byte = Convert.FromBase64String(base64);
            return ImageSource.FromStream(() => new MemoryStream(@byte));
        });
    }
}
