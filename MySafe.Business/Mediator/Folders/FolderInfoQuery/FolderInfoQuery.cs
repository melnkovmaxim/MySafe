using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Folders.FolderInfoQuery
{
    /// <summary>
    /// Получить содержимое ячейки
    /// </summary>
    public class FolderInfoQuery : BearerRequestBase<Folder>
    {
        public int DocumentId { get; set; }

        public FolderInfoQuery(int documentId)
        {
            DocumentId = documentId;
        }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"api/v1/folders/{DocumentId}";
    }
}
