namespace MySafe.Core.Entities.Abstractions
{
    public interface IEntity
    {
        string Error { get; set; }
        public bool HasError { get; }
        byte[] FileBytes { get; set; }
    }
}