using System.Collections.Generic;

namespace MySafe.Presentation.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Document> Documents { get; set; }
    }
}