using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.DestroyTrashSheetCommand
{
    public class RemoveFileFromTrashCommandHandler : RequestHandlerBase<DestroyTrashSheetCommand, EmptyJsonBody, SheetEntity>
    {
        public RemoveFileFromTrashCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}