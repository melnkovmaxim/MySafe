using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Sheets.RestoreTrashSheetCommand
{
    /// <summary>
    /// Восстановить файл из корзины
    /// </summary>
    public class RestoreTrashSheetCommand: BearerRequestBase<Sheet>
    {
        public int SheetId { get; set; }

        public RestoreTrashSheetCommand(string jwtToken, int sheetId) : base(jwtToken)
        {
            SheetId = sheetId;
        }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}";
    }
}
