using Newtonsoft.Json;

namespace Aumy.Devices.Tapo.Models
{
    public class BasicRequest
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public BasicRequestParams Params { get; set; }
    }

    public class BasicRequestParams
    {
        [JsonProperty("request")]
        public string Request { get; set; }
    }
}
