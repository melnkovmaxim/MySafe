using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.DocumentInfoQuery
{
    public class DocumentInfoQueryHandler : RequestHandlerBase<DocumentInfoQuery, Document>
    {
        public DocumentInfoQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
