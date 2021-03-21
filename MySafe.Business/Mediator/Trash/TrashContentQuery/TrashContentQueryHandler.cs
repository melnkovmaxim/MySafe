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
using MySafe.Core.Models.Responses.Abstractions;

namespace MySafe.Business.Mediator.Trash.TrashContentQuery
{
    /// <summary>
    /// Очистить корзину
    /// </summary>
    public class TrashContentQueryHandler: RequestHandlerBase<TrashContentQuery, ResponseList<TrashResponse>>
    {
        public TrashContentQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
