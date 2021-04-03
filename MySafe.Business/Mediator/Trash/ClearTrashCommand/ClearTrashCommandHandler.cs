using AutoMapper;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Models.JsonRequests;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Trash.ClearTrashCommand
{
    public class ClearTrashCommandHandler : RequestHandlerBase<ClearTrashCommand, EmptyJsonBody, EntityBase>
    {
        public ClearTrashCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}