using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.RestoreTrashImageCommand
{
    public class RestoreTrashImageCommandHandler : RequestHandlerBase<RestoreTrashImageCommand, ImagesJsonBody, ImageEntity>
    {
        public RestoreTrashImageCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}