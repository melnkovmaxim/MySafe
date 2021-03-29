using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Sheets.SheetPdfFormatQuery
{
    /// <summary>
    /// Получение файла в формате pdf
    /// </summary>
    public class SheetPdfFormatQuery : BearerRequestBase<Sheet>
    {
        [JsonIgnore]
        public int SheetId { get; set; }

        public SheetPdfFormatQuery(int sheetId)
        {
            SheetId = sheetId;
        }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}";
    }
}
