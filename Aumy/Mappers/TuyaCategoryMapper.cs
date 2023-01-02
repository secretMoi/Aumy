using Aumy.Devices.Shared;
using Aumy.Devices.Shared.DTO;

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