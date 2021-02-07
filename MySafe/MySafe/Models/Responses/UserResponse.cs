using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public class UserResponse : BaseResponse
    {
        public JwtSecurityToken JwtToken { get; set; }
    }
}
