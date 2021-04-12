using MediatR;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Abstractions
{
    public abstract class RequestBase<T> : IRequest<T>
    {
        public abstract Method RequestMethod { get; }

        public abstract string RequestResource { get; }
    }
}