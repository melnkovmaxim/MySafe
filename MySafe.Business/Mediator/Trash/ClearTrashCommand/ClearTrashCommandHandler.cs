using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Trash.ClearTrashCommand
{
    public class ClearTrashCommandHandler : RequestHandlerBase<ClearTrashCommand, ResponseBase>
    {
        public ClearTrashCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}