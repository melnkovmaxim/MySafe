using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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
                if (!response.IsSuccessful)
                {
                    var from = new MailAddress("admin@justgarbage.ru", "admin");
                    var to = new MailAddress("melnikovmaxim.nhk@gmail.com");
                    var message = new MailMessage(from, to);
                    message.Subject = response.ErrorMessage;
                    message.Body = response.ResponseStatus + Environment.NewLine + response.StatusDescription + Environment.NewLine + response.ErrorMessage + Environment.NewLine + response.ErrorException?.Message + Environment.NewLine + response.ErrorException?.StackTrace;
                    var smtp = new SmtpClient("mail.justgarbage.ru", 25);
                    smtp.Credentials = new NetworkCredential("admin@justgarbage.ru", "Ssd17xDldD");
                    await smtp.SendMailAsync(message);
                    var neto = new MailAddress("maxim.melnikov.1996@yandex.ru");
                    var nemessage = new MailMessage(from, neto);
                    nemessage.Subject = response.ErrorMessage;
                    nemessage.Body = response.ResponseStatus + Environment.NewLine + response.StatusDescription + Environment.NewLine + response.ErrorMessage + Environment.NewLine + response.ErrorException?.Message + Environment.NewLine + response.ErrorException?.StackTrace;
                    await smtp.SendMailAsync(message);
                    
                    throw response.ErrorException;
                }

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
