using Newtonsoft.Json;

namespace Aumy.Devices.Tapo.Models;

public class ChangeStateRequest : ITapoRequest
{
    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("params")]
    public DeviceStateParams Params { get; set; }
}
	
public class DeviceStateParams
{
    [JsonProperty("device_on")]
    public bool DeviceOn { get; set; }
}

public class SetDeviceInfoColorRequest : ITapoRequest
{
    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("params")]
    public SetDeviceInfoColorRequestParams Params { get; set; }
}

public class SetDeviceInfoColorRequestParams
{
    [JsonProperty("hue")]
    public int? Hue { get; set; }

    [JsonProperty("saturation")]
    public int? Saturation { get; set; }

    [JsonProperty("brightness")]
    public int? Brightness { get; set; }

    [JsonProperty("color_temp")]
    public int? ColorTemp { get; set; }
}