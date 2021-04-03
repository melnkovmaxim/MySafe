using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.DestroyTrashDocumentCommand
{
    public class DestroyInTrashCommandHandler : RequestHandlerBase<DestroyTrashDocumentCommand, DocumentJsonBody, DocumentEntity>
    {
        public DestroyInTrashCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}