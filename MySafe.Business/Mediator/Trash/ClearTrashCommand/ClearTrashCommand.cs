using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses.Abstractions;
using RestSharp;

namespace MySafe.Business.Mediator.Trash.ClearTrashCommand
{
    /// <summary>
    /// Очистить корзину
    /// </summary>
    public class ClearTrashCommand: BearerRequestBase<ResponseBase>
    {
        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => "/api/v1/trash";
    }
}
