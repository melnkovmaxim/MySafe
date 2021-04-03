using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.RestoreTrashSheetCommand
{
    public class
        RestoreTrashSheetCommandHandler : RequestHandlerBase<RestoreTrashSheetCommand, EmptyJsonBody, SheetEntity>
    {
        public RestoreTrashSheetCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}