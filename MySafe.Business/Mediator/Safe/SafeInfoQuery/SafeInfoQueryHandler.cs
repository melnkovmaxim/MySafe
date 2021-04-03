using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Safe.SafeInfoQuery
{
    public class SafeInfoQueryHandler : RequestHandlerBase<SafeInfoQuery, Core.Entities.Responses.Safe>
    {
        public SafeInfoQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}