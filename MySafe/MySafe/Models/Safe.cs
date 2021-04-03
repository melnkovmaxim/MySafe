using System.Collections.Generic;
using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.Models
{
    public class Safe: PresentationModelBase
    {
        public double Capacity { get; set; }
        public double UsedCapacity { get; set; }
        public List<Folder> Folders { get; set; }
    }
}