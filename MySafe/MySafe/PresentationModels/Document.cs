using System;
using System.Collections.Generic;
using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.Models
{
    public class Document : PresentationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int FolderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}