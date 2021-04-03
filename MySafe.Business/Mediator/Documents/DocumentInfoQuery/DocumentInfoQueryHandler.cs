using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.DocumentInfoQuery
{
    public class DocumentInfoQueryHandler : RequestHandlerBase<DocumentInfoQuery, DocumentJsonBody, DocumentEntity>
    {
        public DocumentInfoQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}