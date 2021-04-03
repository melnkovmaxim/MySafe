using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.DestroyTrashImageCommand
{
    public class DestroyTrashImageCommandHandler : RequestHandlerBase<DestroyTrashImageCommand, ImagesJsonBody, ImageEntity>
    {
        public DestroyTrashImageCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}