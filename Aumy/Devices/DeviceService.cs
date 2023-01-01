using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aumy.Devices.Shared;
using Aumy.Devices.Tuya;
using Aumy.Mappers;

namespace Aumy.Devices;

public class DeviceService
{
	private readonly TuyaService _tuyaService;

	public DeviceService(TuyaService tuyaService)
	{
		_tuyaService = tuyaService;
	}
	
	public async Task<List<DeviceDTO>> DeviceList()
	{
		var tuyaDevices = await _tuyaService.DeviceList();

		return tuyaDevices.Select(TuyaDeviceApiInfoMapper.ToDeviceDTO).ToList();
	}
}