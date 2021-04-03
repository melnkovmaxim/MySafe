using System.Collections.Generic;
using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.Models
{
    public class Folder: PresentationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Document> Documents { get; set; }
    }
}