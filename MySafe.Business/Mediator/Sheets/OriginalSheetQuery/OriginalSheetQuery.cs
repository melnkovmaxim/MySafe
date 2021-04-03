using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.OriginalSheetQuery
{
    /// <summary>
    ///     Получение исходного файла
    /// </summary>
    public class OriginalSheetQuery : BearerRequestBase<Sheet>
    {
        public OriginalSheetQuery(int sheetId)
        {
            SheetId = sheetId;
        }

        public int SheetId { get; set; }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}/download";
    }
}