using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.ChangeImageCommand
{
    public class ChangeImageCommandHandler : RequestHandlerBase<ChangeImageCommand, Image>
    {
        public ChangeImageCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}