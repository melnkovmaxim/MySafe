using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Models.Responses;

namespace MySafe.Mediator.Documents.CreateDocument
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
