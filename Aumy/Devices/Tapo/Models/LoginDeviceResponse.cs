using Newtonsoft.Json;

namespace Aumy.Devices.Tapo.Models
{
    public class LoginDeviceResponse
    {
        [JsonProperty("error_code")]
        public long ErrorCode { get; set; }

        [JsonProperty("result")]
        public LoginDeviceResponseResult Result { get; set; }
    }

    public class LoginDeviceResponseResult
    {
        [JsonProperty("response")]
        public string Response { get; set; }
    }
}