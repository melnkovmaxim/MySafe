using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.UploadImageCommand
{
    public class UploadImageCommandHandler : RequestHandlerBase<UploadImageCommand, Image>
    {
        public UploadImageCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}