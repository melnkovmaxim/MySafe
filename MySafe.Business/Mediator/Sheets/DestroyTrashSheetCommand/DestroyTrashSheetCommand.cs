using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Sheets.DestroyTrashSheetCommand
{
    /// <summary>
    /// Уничтожить файл в корзине
    /// </summary>
    public class DestroyTrashSheetCommand: BearerRequestBase<Sheet>
    {
        public int SheetId { get; set; }

        public DestroyTrashSheetCommand(string jwtToken, int sheetId) : base(jwtToken)
        {
            SheetId = sheetId;
        }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}";
    }
}
