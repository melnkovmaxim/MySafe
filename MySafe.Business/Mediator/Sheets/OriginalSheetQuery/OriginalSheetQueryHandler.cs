using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.OriginalSheetQuery
{
    public class OriginalSheetQueryHandler : RequestHandlerBase<OriginalSheetQuery, EmptyJsonBody, SheetEntity>
    {
        public OriginalSheetQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}