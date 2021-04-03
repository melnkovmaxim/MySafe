using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.CreateDocumentCommand
{
    public class CreateDocumentCommandHandler : RequestHandlerBase<CreateDocumentCommand, DocumentJsonBody, DocumentEntity>
    {
        public CreateDocumentCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}