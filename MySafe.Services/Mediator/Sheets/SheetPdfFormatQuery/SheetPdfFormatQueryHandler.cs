using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.SheetPdfFormatQuery
{
    public class FilePdfFormatQueryHandler : RequestHandlerBase<SheetPdfFormatQuery, EmptyJsonBody, SheetEntity>
    {
        public FilePdfFormatQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}