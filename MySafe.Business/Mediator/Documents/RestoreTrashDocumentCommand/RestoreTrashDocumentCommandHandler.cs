using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.RestoreTrashDocumentCommand
{
    public class RestoreTrashDocumentCommandHandler : RequestHandlerBase<RestoreTrashDocumentCommand, DocumentJsonBody, DocumentEntity>
    {
        public RestoreTrashDocumentCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}