using System.Collections.Generic;

namespace MySafe.Presentation.Models.Abstractions
{
    public class PresentationModelList<T> : List<T>, IPresentationModel
        where T : IPresentationModel
    {
        public string Error { get; set; }
        public bool HasError => !string.IsNullOrEmpty(Error);
        public byte[] FileBytes { get; set; }
    }
}