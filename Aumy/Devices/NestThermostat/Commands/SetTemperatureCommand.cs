using Newtonsoft.Json;

namespace Aumy.Devices.NestThermostat.Commands;

public class SetTemperatureCommand : IGoogleNestCommand
{
	[JsonProperty("command")]
	public string Command { get; set; }

	[JsonProperty("params")]
	public Params Parameters { get; set; }
	
	public class Params
	{
		[JsonProperty("heatCelsius")]
		public double HeatCelsius { get; set; }
	}
}