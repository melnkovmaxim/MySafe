using System.IdentityModel.Tokens.Jwt;
using MySafe.Presentation.Models.Responses.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Presentation.Models.Responses
{
    [JsonObject]
    public class UserResponse : BaseResponse
    {
        public JwtSecurityToken JwtToken { get; set; }
    }
}
