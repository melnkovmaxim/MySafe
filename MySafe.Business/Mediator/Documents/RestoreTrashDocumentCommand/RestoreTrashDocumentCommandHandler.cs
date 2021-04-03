using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.RestoreTrashDocumentCommand
{
    public class RestoreTrashDocumentCommandHandler : RequestHandlerBase<RestoreTrashDocumentCommand, Document>
    {
        public RestoreTrashDocumentCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}