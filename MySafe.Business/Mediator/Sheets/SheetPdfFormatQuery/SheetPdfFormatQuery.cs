using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.SheetPdfFormatQuery
{
    /// <summary>
    ///     Получение файла в формате pdf
    /// </summary>
    public class SheetPdfFormatQuery : BearerRequestBase<Sheet>
    {
        public SheetPdfFormatQuery(int sheetId)
        {
            SheetId = sheetId;
        }

        [JsonIgnore] public int SheetId { get; set; }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}";
    }
}