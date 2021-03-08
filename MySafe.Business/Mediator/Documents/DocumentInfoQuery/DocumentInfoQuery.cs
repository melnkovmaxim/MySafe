using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Documents.DocumentInfoQuery
{
    /// <summary>
    /// Получить информацию о документе
    /// </summary>
    public class DocumentInfoQuery: BearerRequestBase<Document>
    {
        public int DocumentId { get; set; }

        public DocumentInfoQuery(string jwtToken, int documentId) : base(jwtToken)
        {
            DocumentId = documentId;
        }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"api/v1/documents/{DocumentId}";
    }
}
