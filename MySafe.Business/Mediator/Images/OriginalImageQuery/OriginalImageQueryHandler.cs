using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.OriginalImageQuery
{
    public class OriginalImageQueryHandler : RequestHandlerBase<OriginalImageQuery, ImagesJsonBody, ImageEntity>
    {
        public OriginalImageQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}