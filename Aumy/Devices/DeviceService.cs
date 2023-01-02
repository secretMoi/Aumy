using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aumy.Devices.Shared.DTO;
using Aumy.Devices.Tuya;
using Aumy.Mappers;
using Microsoft.Extensions.Options;

namespace Aumy.Devices;

public class DeviceService
{
	private readonly TuyaService _tuyaService;
	private readonly DeviceFactory _deviceFactory;
	private readonly TuyaConfiguration _tuyaConfiguration;

	public DeviceService(TuyaService tuyaService, DeviceFactory deviceFactory,
		IOptions<TuyaConfiguration> options)
	{
		_tuyaService = tuyaService;
		_deviceFactory = deviceFactory;
		_tuyaConfiguration = options.Value;
	}
	
	public async Task<DeviceDTO> GetById(string deviceId)
	{
		var device = _tuyaConfiguration.Devices.FirstOrDefault(x => x.TuyaDeviceId.Equals(deviceId));
		if (device is null) return null;
		
		var tuyaDeviceApiInfo = await _tuyaService.GetDeviceInfoAsync(deviceId);
		
		var tuyaDeviceDTO = TuyaDeviceApiInfoMapper.ToDeviceDTO(tuyaDeviceApiInfo);
		await MapDeviceDTO(tuyaDeviceDTO);
		
		return tuyaDeviceDTO;
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
			
			case DeviceTypeDTO.Socket:
				var socket = _deviceFactory.GetSocket(tuyaDeviceDto.TuyaDevice.DeviceId);
				if(socket is not null)
					tuyaDeviceDto.Socket = await socket.GetSocketAsync(tuyaDeviceDto.TuyaDevice.DeviceId);
				break;
		}
	}
}