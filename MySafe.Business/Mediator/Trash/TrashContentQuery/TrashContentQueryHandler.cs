using MediatR;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Mediator.Abstractions;

namespace MySafe.Business.Mediator.Trash.TrashContentQuery
{
    /// <summary>
    /// Очистить корзину
    /// </summary>
    public class TrashContentQueryHandler: RequestHandlerBase<TrashContentQuery, TrashResponse>
    {
        public TrashContentQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
