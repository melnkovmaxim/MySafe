using MediatR;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses.Abstractions;
using RestSharp;

namespace MySafe.Business.Mediator.Trash.TrashContentQuery
{
    /// <summary>
    /// Получить содержимое корзины
    /// </summary>
    public class TrashContentQuery: BearerRequestBase<ResponseList<TrashResponse>>
    {
        public override Method RequestMethod => Method.GET;
        public override string RequestResource => "/api/v1/trash";
    }
}
