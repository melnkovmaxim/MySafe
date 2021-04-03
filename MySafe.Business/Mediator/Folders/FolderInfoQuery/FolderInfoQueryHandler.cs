using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Folders.FolderInfoQuery
{
    public class FolderInfoQueryHandler : RequestHandlerBase<FolderInfoQuery, Folder>
    {
        public FolderInfoQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}