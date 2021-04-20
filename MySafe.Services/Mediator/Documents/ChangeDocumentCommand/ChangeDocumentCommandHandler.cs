using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.ChangeDocumentCommand
{
    public class ChangeDocumentCommandHandler: RequestHandlerBase<ChangeDocumentCommand, DocumentJsonBody, DocumentEntity>
    {
        public ChangeDocumentCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}