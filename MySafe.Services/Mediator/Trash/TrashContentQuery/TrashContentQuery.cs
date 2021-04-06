using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Trash.TrashContentQuery
{
    /// <summary>
    ///     Получить содержимое корзины
    /// </summary>
    public class TrashContentQuery : BearerRequestBase<EntityList<TrashEntity>>
    {
        public override Method RequestMethod => Method.GET;
        public override string RequestResource => "/api/v1/trash";
    }
}