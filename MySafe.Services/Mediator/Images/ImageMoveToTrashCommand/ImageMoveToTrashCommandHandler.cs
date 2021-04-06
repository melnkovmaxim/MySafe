using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.ImageMoveToTrashCommand
{
    public class
        ImageMoveToTrashCommandHandler : RequestHandlerBase<ImageMoveToTrashCommand, ImagesJsonBody, ImageEntity>
    {
        public ImageMoveToTrashCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}