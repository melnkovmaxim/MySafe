using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;

namespace MySafe.Business.Mediator.Sheets.OriginalSheetQuery
{
    public class OriginalSheetQueryHandler: RequestHandlerBase<OriginalSheetQuery, Sheet>
    {
        public OriginalSheetQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
