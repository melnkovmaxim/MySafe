using DryIoc;
using Newtonsoft.Json;

namespace MySafe.Models.Responses
{
    [JsonObject]
    public abstract class BaseResponse: IResponse
    {
        [JsonProperty("error")]
        public string Error { get; }
        public bool HasError => !string.IsNullOrEmpty(Error);
    }
}
