using System;

namespace MySafe.Presentation.Models
{
    public class Trash : Attachment
    {
        public string Location { get; set; }
        public int? FolderId { get; set; }
        public bool ConstainsAttachments { get; set; }
        public DateTime? TrashedAt { get; set; }
        public string Content { get; set; }
        public bool IsFolder => FolderId != null;
        public bool IsDocument => FolderId == null;
    }
}