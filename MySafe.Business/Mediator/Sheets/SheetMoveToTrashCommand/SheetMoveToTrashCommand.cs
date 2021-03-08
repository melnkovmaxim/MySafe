using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Sheets.SheetMoveToTrashCommand
{
    /// <summary>
    /// Отправить файл в корзину
    /// </summary>
    public class SheetMoveToTrashCommand: BearerRequestBase<Sheet>
    {
        public int SheetId { get; set; }

        public SheetMoveToTrashCommand(string jwtToken, int sheetId) : base(jwtToken)
        {
            SheetId = sheetId;
        }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}/trash";
    }
}
