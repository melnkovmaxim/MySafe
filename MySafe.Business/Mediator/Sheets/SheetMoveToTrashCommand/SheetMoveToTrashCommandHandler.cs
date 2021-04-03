using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.SheetMoveToTrashCommand
{
    public class MoveFileToTrashCommandHandler : RequestHandlerBase<SheetMoveToTrashCommand, Sheet>
    {
        public MoveFileToTrashCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}