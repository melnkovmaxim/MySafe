using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.CreateDocumentCommand
{
    public class CreateDocumentCommandHandler : RequestHandlerBase<CreateDocumentCommand, Document>
    {
        public CreateDocumentCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}