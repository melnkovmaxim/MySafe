using MySafe.Core.Entities.Abstractions;
using Newtonsoft.Json;

namespace MySafe.Core.Models.Responses
{
    [JsonObject]
    public class UserEntity : EntityBase, IEntity
    {
        public string JwtToken { get; set; }
    }
}