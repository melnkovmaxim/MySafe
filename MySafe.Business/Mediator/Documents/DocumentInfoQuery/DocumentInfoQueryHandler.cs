using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.DocumentInfoQuery
{
    public class DocumentInfoQueryHandler : RequestHandlerBase<DocumentInfoQuery, Document>
    {
        public DocumentInfoQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}