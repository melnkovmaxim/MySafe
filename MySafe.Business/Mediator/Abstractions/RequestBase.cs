using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Abstractions
{
    public abstract class RequestBase<T>: IRequest<T>
    {
        [JsonIgnore]
        public abstract Method RequestMethod { get; }

        [JsonIgnore]
        public abstract string RequestResource { get; }
    }
}
