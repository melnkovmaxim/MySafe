using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses.Abstractions;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Trash.TrashContentQuery
{
    /// <summary>
    ///     Очистить корзину
    /// </summary>
    public class TrashContentQueryHandler : RequestHandlerBase<TrashContentQuery, ResponseList<TrashResponse>>
    {
        public TrashContentQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}