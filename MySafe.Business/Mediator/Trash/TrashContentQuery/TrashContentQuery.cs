using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses.Abstractions;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Trash.TrashContentQuery
{
    /// <summary>
    ///     Получить содержимое корзины
    /// </summary>
    public class TrashContentQuery : BearerRequestBase<ResponseList<TrashResponse>>
    {
        public override Method RequestMethod => Method.GET;
        public override string RequestResource => "/api/v1/trash";
    }
}