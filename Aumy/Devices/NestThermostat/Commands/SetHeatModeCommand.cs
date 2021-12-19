using Newtonsoft.Json;

namespace Aumy.Devices.NestThermostat.Commands;

public class SetHeatModeCommand
{
	[JsonProperty("command")]
	public string Command { get; set; }

	[JsonProperty("params")]
	public Params Parameters { get; set; }
	
	public class Params
	{
		[JsonProperty("mode")]
		public string Mode { get; set; }
	}
}