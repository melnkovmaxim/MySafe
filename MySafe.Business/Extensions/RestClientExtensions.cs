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

                //TODO !!! Добавить CancellationToken при переходе на другую страницу отменять таск !!!
                if (!response.IsSuccessful) throw response.ErrorException;

                if (response.ContentType.Contains(ContentType.Json) == true)
                {
                    cmdResponse = JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    cmdResponse.FileBytes = response.RawBytes;
                }

                if (cmdResponse is User userResponse)
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
