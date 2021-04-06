using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Folders.FolderInfoQuery
{
    public class FolderInfoQueryHandler : RequestHandlerBase<FolderInfoQuery, FolderJsonBody, FolderEntity>
    {
        public FolderInfoQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}