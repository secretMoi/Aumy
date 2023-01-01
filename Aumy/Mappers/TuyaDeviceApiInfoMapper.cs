using Aumy.Devices.Shared;
using com.clusterrr.TuyaNet;

namespace Aumy.Mappers;

public static class TuyaDeviceApiInfoMapper
{
	public static DeviceDTO ToDeviceDTO(TuyaDeviceApiInfo tuyaDeviceApiInfo)
	{
		return new DeviceDTO
		{
			DeviceType = TuyaCategoryMapper.GetTuyaCategory(tuyaDeviceApiInfo.Category),
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
}