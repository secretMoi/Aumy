using Aumy.Devices.Shared;

namespace Aumy.Mappers;

public static class TuyaCategoryMapper
{
	public static DeviceTypeDTO GetTuyaCategory(string category)
	{
		return category switch
		{
			"kg" => DeviceTypeDTO.Switch,
			"cz" => DeviceTypeDTO.Socket,
			"tgkg" => DeviceTypeDTO.DimmerSwitch,
			_ => default
		};
	}
}