using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.SheetMoveToTrashCommand
{
    public class MoveFileToTrashCommandHandler : RequestHandlerBase<SheetMoveToTrashCommand, EmptyJsonBody, SheetEntity>
    {
        public MoveFileToTrashCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}