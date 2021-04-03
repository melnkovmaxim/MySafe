using MySafe.Core.Entities.Responses.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Models.Responses
{
    [JsonObject]
    public class User : ResponseBase
    {
        public string JwtToken { get; set; }
    }
}