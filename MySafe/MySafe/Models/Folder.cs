using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Presentation.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Document> Documents { get; set; }
    }
}
