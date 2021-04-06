using System.Linq;
using RestSharp;

namespace MySafe.Services.Extensions
{
    public static class RestResponseExtensions
    {
        public static string GetJwtToken(this IRestResponse response)
        {
            var jwtToken = response.Headers
                .FirstOrDefault(x => x.Name == "Authorization")?.Value
                .ToString()
                .Replace("Bearer ", string.Empty);

            return jwtToken;
        }
    }
}