using Newtonsoft.Json;

namespace Aumy.Devices.Tapo.Models
{
    public class LoginDeviceRequest
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public LoginDeviceRequestParams Params { get; set; }

        [JsonProperty("requestTimeMils")]
        public long RequestTimeMils { get; set; }
    }

    public class LoginDeviceRequestParams
    {
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}