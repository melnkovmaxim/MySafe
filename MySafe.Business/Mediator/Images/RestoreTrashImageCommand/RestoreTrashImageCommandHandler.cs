using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.RestoreTrashImageCommand
{
    public class RestoreTrashImageCommandHandler : RequestHandlerBase<RestoreTrashImageCommand, Image>
    {
        public RestoreTrashImageCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}