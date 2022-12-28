using Newtonsoft.Json;

namespace Aumy.Devices.Tuya;

public class DeviceInfoRequest
{
	[JsonProperty("gwId")]
	public string GwId { get; set; }
	
	[JsonProperty("devId")]
	public string DevId { get; set; }
	
	[JsonProperty("uid")]
	public string Uid { get; set; }
	
	[JsonProperty("t")]
	public string Timestamp { get; set; }
}