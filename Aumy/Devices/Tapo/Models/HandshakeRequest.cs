using Newtonsoft.Json;

namespace Aumy.Devices.Tapo.Models
{
    public class HandshakeRequest
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public HandshakeRequestParams Params { get; set; }

        [JsonProperty("requestTimeMils")]
        public long RequestTimeMils { get; set; }
    }

    public class HandshakeRequestParams
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
