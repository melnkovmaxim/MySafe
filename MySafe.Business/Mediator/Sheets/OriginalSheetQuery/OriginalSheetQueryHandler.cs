using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.OriginalSheetQuery
{
    public class OriginalSheetQueryHandler : RequestHandlerBase<OriginalSheetQuery, Sheet>
    {
        public OriginalSheetQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}