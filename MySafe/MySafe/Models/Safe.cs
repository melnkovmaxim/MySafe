using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Presentation.Models
{
    public class Safe
    {
        public double Capacity { get; set; }
        public double UsedCapacity { get; set; }
        public List<Folder> Folders { get; set; }
    }
}
