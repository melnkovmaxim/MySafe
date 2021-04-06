using Newtonsoft.Json;

namespace MySafe.Services.Mediator.Abstractions
{
    public abstract class BearerRequestBase<T> : RequestBase<T>
    {
        [JsonIgnore] public string JwtToken { get; set; }
    }
}