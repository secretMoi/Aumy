using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Aumy.Devices.NestThermostat.Models;

public class HeatMode
{
	private readonly ICollection<string> _existingHeatModes = new Collection<string>();

	public HeatMode()
	{
		_existingHeatModes.Add("HEAT");
		_existingHeatModes.Add("COOL");
		_existingHeatModes.Add("HEATCOOL");
	}

	public bool IsValidHeatModeValue(string heatModeValue)
	{
		return _existingHeatModes.Any(valueInList => heatModeValue == valueInList);
	}
}