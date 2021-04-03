using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Images.OriginalImageQuery
{
    public class OriginalImageQueryHandler : RequestHandlerBase<OriginalImageQuery, Image>
    {
        public OriginalImageQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}