using Newtonsoft.Json;

namespace MySafe.Core.Models.JsonRequests
{
    public class UserJsonBody : JsonObjectBase, IJsonBody
    {
        [JsonProperty("login")] public string Login { get; set; }

        [JsonProperty("password")] public string Password { get; set; }

        [JsonProperty("password_confirmation")]
        public string PasswordConfirmation { get; set; }

        [JsonProperty("code")] public string EmailCode { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }

        [JsonProperty("user_agreement")] public bool? UserAgreement { get; set; }

        [JsonProperty("refresh_token")] public string RefreshToken { get; set; }
    }
}