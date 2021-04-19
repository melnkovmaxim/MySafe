using MySafe.Core.Commands;

namespace MySafe.Presentation.Models.Abstractions
{
    public class PresentationModelBase : BindableBase, IPresentationModel
    {
        public string Error { get; set; }
        public bool HasError => !string.IsNullOrEmpty(Error);
        public byte[] FileBytes { get; set; }
    }
}