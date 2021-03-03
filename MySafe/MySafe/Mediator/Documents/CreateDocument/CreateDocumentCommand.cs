using MediatR;
using MySafe.Presentation.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace MySafe.Presentation.Mediator.Documents.CreateDocument
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
