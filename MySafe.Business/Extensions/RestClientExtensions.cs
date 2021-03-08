using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using RestSharp.Serialization;

namespace MySafe.Business.Extensions
{
    public static class RestClientExtensions
    {
        public static async Task<T> SendAndGetResponseAsync<T>(this IRestClient client,
            IRestRequest request, CancellationToken cancellationToken) 
            where T : IResponse
        {
            var cmdResponse = (T) Activator.CreateInstance(typeof(T));

            try
            {
                var response = await client.ExecuteAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                if (!response.ContentType.Contains(ContentType.Json))
                {
                    cmdResponse.FileBytes = response.RawBytes;
                    return cmdResponse;
                }

                if (typeof(T) == typeof(IArrayResponse<IResponse>) && response.ContentType == ContentType.Json)
                {
                    var responseArray = (IArrayResponse<IResponse>) cmdResponse;
                    responseArray.ResponseArray = JsonConvert.DeserializeObject<IResponse[]>(response.Content);
                }
                else
                {
                    cmdResponse = JsonConvert.DeserializeObject<T>(response.Content);
                }

                if (response.IsSuccessful && cmdResponse is User userResponse)
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
