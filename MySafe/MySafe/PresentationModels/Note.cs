using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.Models
{
    public class Note : PresentationModelBase
    {
        public int Id { get; set; }
        public string ClippedContent { get; set; }
    }
}