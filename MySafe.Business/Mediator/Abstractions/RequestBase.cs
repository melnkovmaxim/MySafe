using MediatR;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Abstractions
{
    public abstract class RequestBase<T> : IRequest<T>
    {
        [JsonIgnore] public abstract Method RequestMethod { get; }

        [JsonIgnore] public abstract string RequestResource { get; }
    }
}