using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Folders.FolderInfoQuery
{
    /// <summary>
    /// Получить содержимое ячейки
    /// </summary>
    public class FolderInfoQuery : AuthorizedRequestBase<FolderResponse>
    {
        public int DocumentId { get; set; }

        public FolderInfoQuery(string jwtToken, int documentId) : base(jwtToken)
        {
            DocumentId = documentId;
        }
    }
}
