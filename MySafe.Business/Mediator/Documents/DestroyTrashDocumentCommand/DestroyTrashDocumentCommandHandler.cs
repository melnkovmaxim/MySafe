using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.DestroyTrashDocumentCommand
{
    public class DestroyInTrashCommandHandler : RequestHandlerBase<DestroyTrashDocumentCommand, Document>
    {
        public DestroyInTrashCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}