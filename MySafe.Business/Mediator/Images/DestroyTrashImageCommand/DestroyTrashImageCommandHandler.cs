using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.DestroyTrashImageCommand
{
    public class DestroyTrashImageCommandHandler : RequestHandlerBase<DestroyTrashImageCommand, Image>
    {
        public DestroyTrashImageCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}