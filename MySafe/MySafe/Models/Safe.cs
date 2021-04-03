using System.Collections.Generic;

namespace MySafe.Presentation.Models
{
    public class Safe
    {
        public double Capacity { get; set; }
        public double UsedCapacity { get; set; }
        public List<Folder> Folders { get; set; }
    }
}