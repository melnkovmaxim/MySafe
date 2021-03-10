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
            var commandResponse = (T) Activator.CreateInstance(typeof(T));

            try
            {
                var response = await client.ExecuteAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                //TODO !!! Добавить CancellationToken при переходе на другую страницу отменять таск !!!
                if (!response.IsSuccessful) throw response.ErrorException;

                if (response.ContentType.Contains(ContentType.Json) == true)
                {
                    commandResponse = JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    commandResponse.FileBytes = response.RawBytes;
                }

                if (commandResponse is User userResponse)
                {
                    userResponse.JwtToken = response.GetJwtToken();
                }
            }
            catch (Exception ex)
            {
                commandResponse.Error = ex.Message;
            }

            return commandResponse;
        }
    }
}
