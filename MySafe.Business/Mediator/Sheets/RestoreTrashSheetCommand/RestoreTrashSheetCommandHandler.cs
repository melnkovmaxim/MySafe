using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.RestoreTrashSheetCommand
{
    public class RestoreTrashSheetCommandHandler : RequestHandlerBase<RestoreTrashSheetCommand, Sheet>
    {
        public RestoreTrashSheetCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}