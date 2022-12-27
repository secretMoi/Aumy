using Newtonsoft.Json;

namespace Aumy.Devices.Tapo.Models
{
    public class LoginDeviceResponseDecrypted
    {
        [JsonProperty("error_code")]
        public long ErrorCode { get; set; }

        [JsonProperty("result")]
        public LoginDeviceResponseDecryptedResult Result { get; set; }
    }

    public class LoginDeviceResponseDecryptedResult
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}