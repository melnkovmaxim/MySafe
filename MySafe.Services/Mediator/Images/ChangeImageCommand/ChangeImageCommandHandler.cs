using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.ChangeImageCommand
{
    public class ChangeImageCommandHandler : RequestHandlerBase<ChangeImageCommand, ImagesJsonBody, ImageEntity>
    {
        public ChangeImageCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}