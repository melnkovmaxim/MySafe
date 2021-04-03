using MySafe.Core.Entities.Abstractions;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Trash.ClearTrashCommand
{
    /// <summary>
    ///     Очистить корзину
    /// </summary>
    public class ClearTrashCommand : BearerRequestBase<EntityBase>
    {
        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => "/api/v1/trash";
    }
}