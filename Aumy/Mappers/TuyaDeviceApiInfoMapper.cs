using Aumy.Devices.Shared;
using Aumy.Devices.Shared.DTO;
using com.clusterrr.TuyaNet;

namespace Aumy.Mappers;

public static class TuyaDeviceApiInfoMapper
{
	public static DeviceDTO ToDeviceDTO(TuyaDeviceApiInfo tuyaDeviceApiInfo)
	{
		var deviceType = TuyaCategoryMapper.GetTuyaCategory(tuyaDeviceApiInfo.Category);
		return new DeviceDTO
		{
			DeviceType = deviceType,
			Name = tuyaDeviceApiInfo.Name,
			Address = tuyaDeviceApiInfo.Ip,
			Icon = tuyaDeviceApiInfo.Icon,
			IsOnline = tuyaDeviceApiInfo.Online,
			IsTuyaDevice = true,
			TuyaDevice = new TuyaDeviceDTO
			{
				DeviceId = tuyaDeviceApiInfo.Id
			}
		};
	}

	// public static SwitchDTO ToSwitchDTO(TuyaDeviceApiInfo tuyaDeviceApiInfo, DeviceTypeDTO deviceType)
	// {
	// 	if (deviceType != DeviceTypeDTO.Switch) return null;
	// 	
	// 	
	//
	// 	return new SwitchDTO
	// 	{
	// 		State = tuyaDeviceApiInfo.
	// 	};
	// }
}