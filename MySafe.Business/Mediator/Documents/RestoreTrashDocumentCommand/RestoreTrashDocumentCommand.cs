﻿using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Documents.RestoreTrashDocumentCommand
{
    /// <summary>
    /// Восстановить документ из корзины
    /// </summary>
    public class RestoreTrashDocumentCommand: BearerRequestBase<Document>
    {
        public int DocumentId { get; set; }

        public RestoreTrashDocumentCommand(string jwtToken) : base(jwtToken)
        {
        }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/documents/{DocumentId}/restore";
    }
}
