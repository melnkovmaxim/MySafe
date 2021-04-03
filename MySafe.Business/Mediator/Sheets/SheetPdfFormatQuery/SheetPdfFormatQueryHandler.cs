using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.SheetPdfFormatQuery
{
    public class FilePdfFormatQueryHandler : RequestHandlerBase<SheetPdfFormatQuery, Sheet>
    {
        public FilePdfFormatQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}