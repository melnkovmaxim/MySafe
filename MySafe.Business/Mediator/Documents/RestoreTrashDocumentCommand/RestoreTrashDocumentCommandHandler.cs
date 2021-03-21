using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.RestoreTrashDocumentCommand
{
    public class RestoreTrashDocumentCommandHandler: RequestHandlerBase<RestoreTrashDocumentCommand, Document>
    {
        public RestoreTrashDocumentCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
