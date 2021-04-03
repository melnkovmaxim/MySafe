using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.SheetMoveToTrashCommand
{
    /// <summary>
    ///     Отправить файл в корзину
    /// </summary>
    public class SheetMoveToTrashCommand : BearerRequestBase<Sheet>
    {
        public SheetMoveToTrashCommand(int sheetId)
        {
            SheetId = sheetId;
        }

        public int SheetId { get; set; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}/trash";
    }
}