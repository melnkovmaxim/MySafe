using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MySafe.Business.Extensions
{
    public static class RestResponseExtensions
    {
        public static string GetJwtToken(this IRestResponse response)
        {
            var jwtToken = response.Headers
                .FirstOrDefault(x => x.Name == "Authorization")?.Value
                .ToString()
                .Replace("Bearer ", String.Empty);

            return jwtToken;
        }
    }
}
