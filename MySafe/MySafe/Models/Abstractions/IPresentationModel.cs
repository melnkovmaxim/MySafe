using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Presentation.Models.Abstractions
{
    public interface IPresentationModel
    {
        public string Error { get; set; }
        public bool HasError { get; }
        public byte[] FileBytes { get; set; }
    }
}
