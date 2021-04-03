using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Trash.ClearTrashCommand
{
    /// <summary>
    ///     Очистить корзину
    /// </summary>
    public class ClearTrashCommand : BearerRequestBase<ResponseBase>
    {
        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => "/api/v1/trash";
    }
}