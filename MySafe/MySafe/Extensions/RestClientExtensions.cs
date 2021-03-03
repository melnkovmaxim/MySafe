using MySafe.Presentation.Models.Responses;
using MySafe.Presentation.Models.Responses.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace MySafe.Presentation.Extensions
{
    public static class RestClientExtensions
    {
        public static async Task<T> GetResponseAsync<T>(this IRestClient client,
            IRestRequest request, CancellationToken cancellationToken) 
            where T : IResponse
        {
            var cmdResponse = (T) Activator.CreateInstance(typeof(T));

            try
            {
                var response = await client.ExecuteAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                if (response.ContentType.Contains("application/json"))
                {
                    cmdResponse = JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    cmdResponse.FileBytes = response.RawBytes;
                }

                if (response.IsSuccessful && cmdResponse is UserResponse userResponse)
                {
                    var jwtToken = new JwtSecurityTokenHandler()
                        .GetJwtTokenFromResponse(response);

                    userResponse.JwtToken = jwtToken;
                }
            }
            catch (Exception ex)
            {
                cmdResponse.Error = ex.Message;
            }

            return cmdResponse;
        }
    }
}
