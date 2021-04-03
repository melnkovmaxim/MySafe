using System;
using System.Collections.Generic;

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