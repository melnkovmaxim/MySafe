using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using MySafe.Core;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Core.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;

namespace MySafe.Services.Extensions
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
                if (!response.IsSuccessful)
                {
                    var properties = new Dictionary<string, string>
                    {
                        {"User login", MySafeApp.Resources.UserLogin},
                        {"Url", response.ResponseUri.AbsoluteUri},
                        {"Request method", request.Method.ToString()},
                        {"Status code", response.StatusDescription},
                        {"Content", response.Content},
                        {"Content-Type", response.ContentType},
                        {"Body", request.Body.Value.ToString()},
                        {"ErrorMessage", response.ErrorMessage},
                        {"ErrorExceptionMessage", response.ErrorException?.Message},
                        {"ErrorExceptionStackTrace", response.ErrorException?.StackTrace}
                    };

                    foreach (var param in request.Parameters)
                        properties.Add($"Parameter {param.Name}", param.Value.ToString());

                    foreach (var file in request.Files) properties.Add($"File {file.FileName}", file.ContentType);

                    Crashes.TrackError(new Exception("RestSharp http request error"), properties);

                    //throw response?.ErrorException;
                }

                if (response.ContentType.Contains(ContentType.Json))
                    commandResponse = JsonConvert.DeserializeObject<T>(response.Content);
                else
                    commandResponse.FileBytes = response.RawBytes;

                if (commandResponse is User userResponse) userResponse.JwtToken = response.GetJwtToken();
            }
            catch (Exception ex)
            {
                commandResponse.Error = ex.Message;
            }

            return commandResponse;
        }
    }
}