using Fody;
using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;

namespace MySafe.Business.Mediator.Safe.SafeInfoQuery
{
    public class SafeInfoQueryHandler : RequestHandlerBase<SafeInfoQuery, Core.Entities.Responses.Safe>
    {
        public SafeInfoQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
