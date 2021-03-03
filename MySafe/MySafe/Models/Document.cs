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
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FolderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
