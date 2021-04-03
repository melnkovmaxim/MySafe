namespace MySafe.Services.Mediator.Abstractions
{
    public abstract class RequestUploadBase<T> : BearerRequestBase<T>
    {
        public abstract string FileName { get; }
        public abstract string ContentType { get; }
        public abstract byte[] FileBytes { get; }
    }
}