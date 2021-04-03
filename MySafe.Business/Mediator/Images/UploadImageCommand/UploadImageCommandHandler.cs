using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.UploadImageCommand
{
    public class UploadImageCommandHandler : RequestHandlerBase<UploadImageCommand, ImagesJsonBody, ImageEntity>
    {
        public UploadImageCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}