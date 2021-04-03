using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.RestoreTrashSheetCommand
{
    /// <summary>
    ///     Восстановить файл из корзины
    /// </summary>
    public class RestoreTrashSheetCommand : BearerRequestBase<SheetEntity>
    {
        public RestoreTrashSheetCommand(int sheetId)
        {
            SheetId = sheetId;
        }

        public int SheetId { get; set; }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}";
    }
}