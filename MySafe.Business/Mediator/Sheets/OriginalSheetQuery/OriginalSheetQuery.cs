using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Sheets.OriginalSheetQuery
{
    /// <summary>
    /// Получение исходного файла
    /// </summary>
    public class OriginalSheetQuery: BearerRequestBase<Sheet>
    {
        public int SheetId { get; set; }

        public OriginalSheetQuery(string jwtToken, int sheetId) : base(jwtToken)
        {
            SheetId = sheetId;
        }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"/api/v1/sheets/{SheetId}/download";
    }
}
