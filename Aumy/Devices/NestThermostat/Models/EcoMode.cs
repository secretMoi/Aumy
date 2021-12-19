using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Aumy.Devices.NestThermostat.Models;

public class EcoMode
{
	private readonly ICollection<string> _existingEcoModes = new Collection<string>();

	public EcoMode()
	{
		_existingEcoModes.Add("MANUAL_ECO");
		_existingEcoModes.Add("OFF");
	}

	public bool IsValidEcoModeValue(string ecoModeValue)
	{
		return _existingEcoModes.Any(valueInList => ecoModeValue == valueInList);
	}
}