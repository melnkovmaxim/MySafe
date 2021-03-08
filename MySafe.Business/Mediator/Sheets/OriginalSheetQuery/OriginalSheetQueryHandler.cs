using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;

namespace MySafe.Business.Mediator.Sheets.OriginalSheetQuery
{
    public class OriginalSheetQueryHandler: IRequestHandler<OriginalSheetQuery, Sheet>
    {
        private readonly IRestClient _restClient;
        
        public OriginalSheetQueryHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Sheet> Handle(OriginalSheetQuery request, CancellationToken cancellationToken)
        {
            var httpRequest = new RestRequest($"/api/v1/sheets/{request.SheetId}/download", Method.GET);
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var response = await _restClient.GetResponseAsync<Sheet>(httpRequest, cancellationToken);

            return response;
        }
    }
}
