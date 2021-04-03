using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.DestroyTrashSheetCommand
{
    public class RemoveFileFromTrashCommandHandler : RequestHandlerBase<DestroyTrashSheetCommand, Sheet>
    {
        public RemoveFileFromTrashCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}