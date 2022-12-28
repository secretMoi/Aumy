using Newtonsoft.Json;

namespace Aumy.Devices.Tuya.Devices.MoesDimmerSwitch;

public class MoesDimmerSwitchRequest
{
	[JsonProperty("dps")]
	public Dps Dps { get; set; }
}

public class Dps
{
	[JsonProperty("1")]
	public bool? State { get; set; }

	[JsonProperty("2")]
	public int? Brightness { get; set; }

	[JsonProperty("3")]
	public int? MinBrightness { get; set; }

	[JsonProperty("4")]
	public string? LedType { get; set; }

	[JsonProperty("13")]
	public string? WorkMode { get; set; }
}