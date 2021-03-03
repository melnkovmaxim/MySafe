using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Documents.CreateDocument
{
    /// <summary>
    /// Создание папки (документа) в ячейке сейфа
    /// </summary>
    public class CreateDocumentCommand: IRequest<DocumentResponse>
    {
        public JwtSecurityToken JwtToken { get; set; }
        public int FolderId { get; set; }

        public CreateDocumentCommand(JwtSecurityToken jwtToken, int folderId)
        {
            JwtToken = jwtToken;
            FolderId = folderId;
        }
    }
}
