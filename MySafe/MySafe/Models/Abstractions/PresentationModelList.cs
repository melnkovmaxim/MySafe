using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Presentation.Models.Abstractions
{
    public class PresentationModelList<T>: List<T>, IPresentationModel
        where T: IPresentationModel
    {
        public string Error { get; set; }
        public bool HasError => !string.IsNullOrEmpty(Error);
        public byte[] FileBytes { get; set; }
    }
}
