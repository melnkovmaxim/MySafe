using AutoMapper;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Trash.TrashContentQuery
{
    /// <summary>
    ///     Очистить корзину
    /// </summary>
    public class
        TrashContentQueryHandler : RequestHandlerBase<TrashContentQuery, EmptyJsonBody, EntityList<TrashEntity>>
    {
        public TrashContentQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}