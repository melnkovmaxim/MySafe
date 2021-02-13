using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Models.Responses;
using MySafe.Repositories.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Extensions
{
    public static class RestClientExtensions
    {
        public static async Task<T> GetResponseAsync<T>(this IRestClient client,
            IRestRequest request, CancellationToken cancellationToken, Action actionOnSuccessful = null) 
            where T : IResponse
        {
            var cmdResponse = (T) Activator.CreateInstance(typeof(T));

            try
            {
                var response = await client.ExecuteAsync(request, cancellationToken)
                    .ConfigureAwait(false);
                
                cmdResponse = JsonConvert.DeserializeObject<T>(response.Content);

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
