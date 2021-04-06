using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.DestroyTrashSheetCommand
{
    /// <summary>
    ///     Уничтожить файл в корзине
    /// </summary>
    public class DestroyTrashSheetCommand : BearerRequestBase<SheetEntity>
    {
        public DestroyTrashSheetCommand(int sheetId)
        {
            SheetId = sheetId;
        }

        public int SheetId { get; set; }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}";
    }
}