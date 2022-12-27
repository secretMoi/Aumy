using Newtonsoft.Json;

namespace Aumy.Devices.Tapo.Models
{
    public class HandshakeResponse
    {
        [JsonProperty("error_code")]
        public long ErrorCode { get; set; }

        [JsonProperty("result")]
        public HandshakeResponseResult Result { get; set; }
    }

    public class HandshakeResponseResult
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
