using System.Collections.Generic;
using System.Threading.Tasks;
using Aumy.Devices.Shared;
using Aumy.Devices.Tuya;
using Aumy.Mappers;

namespace Aumy.Devices;

public class DeviceService
{
	private readonly TuyaService _tuyaService;
	private readonly DeviceFactory _deviceFactory;

	public DeviceService(TuyaService tuyaService, DeviceFactory deviceFactory)
	{
		_tuyaService = tuyaService;
		_deviceFactory = deviceFactory;
	}
	
	public async Task<List<DeviceDTO>> DeviceList()
	{
		var tuyaDevices = await _tuyaService.DeviceList();

		var tuyaDevicesDTO = new List<DeviceDTO>();
		foreach (var tuyaDevice in tuyaDevices)
		{
			var tuyaDeviceDTO = TuyaDeviceApiInfoMapper.ToDeviceDTO(tuyaDevice);
			await MapDeviceDTO(tuyaDeviceDTO);
			tuyaDevicesDTO.Add(tuyaDeviceDTO);
		}

		return tuyaDevicesDTO;
	}

	private async Task MapDeviceDTO(DeviceDTO tuyaDeviceDto)
	{
		switch (tuyaDeviceDto.DeviceType)
		{
			case DeviceTypeDTO.Switch:
			case DeviceTypeDTO.DimmerSwitch:
				var dimmerSwitch = _deviceFactory.GetDimmerSwitch(tuyaDeviceDto.TuyaDevice.DeviceId);
				if(dimmerSwitch is not null)
					tuyaDeviceDto.Switch = await dimmerSwitch.GetSwitchInformationAsync(tuyaDeviceDto.TuyaDevice.DeviceId);
				break;
		}
	}
}