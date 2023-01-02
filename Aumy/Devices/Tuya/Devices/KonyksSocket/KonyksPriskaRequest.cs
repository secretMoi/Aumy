using Newtonsoft.Json;

namespace Aumy.Devices.Tuya.Devices.KonyksSocket;

public class KonyksPriskaRequest
{
	[JsonProperty("dps")]
	public Dps Dps { get; set; }
}

public class Dps
{
	[JsonProperty("1")]
	public bool? State { get; set; }

	[JsonProperty("9")]
	public int? Countdown { get; set; }

	[JsonProperty("17")]
	public int? IncreasePower { get; set; }

	[JsonProperty("18")]
	public string? Current { get; set; }

	[JsonProperty("19")]
	public string? Power { get; set; }

	[JsonProperty("20")]
	public string? Voltage { get; set; }
}