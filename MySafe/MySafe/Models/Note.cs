using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.Models
{
    public class Note: PresentationModelBase
    {
        public int Id { get; set; }
        public string ClippedContent { get; set; }
    }
}
