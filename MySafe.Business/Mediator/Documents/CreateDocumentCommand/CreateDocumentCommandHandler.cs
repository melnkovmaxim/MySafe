﻿using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;

namespace MySafe.Business.Mediator.Documents.CreateDocumentCommand
{
    public class CreateDocumentCommandHandler: AuthorizedRequestHandlerBase<CreateDocumentCommand, Document>
    {
        public CreateDocumentCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
