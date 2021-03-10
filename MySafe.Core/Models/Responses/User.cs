using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Core.Entities.Responses
{
    [JsonObject]
    public class User : ResponseBase
    {
        public string JwtToken { get; set; }
    }
}
