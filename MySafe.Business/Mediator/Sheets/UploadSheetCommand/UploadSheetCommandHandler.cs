using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.UploadSheetCommand
{
    public class UploadFileCommandHandler : RequestHandlerBase<UploadSheetCommand, Sheet>
    {
        public UploadFileCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}