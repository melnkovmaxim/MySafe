using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Safe.SafeInfoQuery
{
    public class SafeInfoQueryHandler : RequestHandlerBase<SafeInfoQuery, SafeJsonBody, SafeEntity>
    {
        public SafeInfoQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}