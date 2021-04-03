namespace MySafe.Presentation.Models.Abstractions
{
    public interface IPresentationModel
    {
        public string Error { get; set; }
        public bool HasError { get; }
        public byte[] FileBytes { get; set; }
    }
}