using System;
using System.Threading.Tasks;
using Aumy.Devices.Tuya.Devices.Interfaces;
using com.clusterrr.TuyaNet;

namespace Aumy.Devices.Tuya.Devices;

public class MoesDimmerSwitch : IDimmerSwitch
{
	private readonly TuyaService _tuyaService;
	private TuyaDeviceApiInfo _tuyaDeviceApiInfo;
	private TuyaDevice _tuyaDevice;

	public MoesDimmerSwitch(TuyaService tuyaService)
	{
		_tuyaService = tuyaService;
	}

	public async Task TurnOnAsync(string deviceId)
	{
		var tuyaDevice = await _tuyaService.GetTuyaDeviceFromIdAsync(deviceId);
		
		var response = await tuyaDevice.SendAsync(TuyaCommand.CONTROL, tuyaDevice.FillJson("{\"dps\":{\"1\":true}}"));
		Console.WriteLine($"Response JSON: {response.JSON}");
	}

	public async Task TurnOffAsync(string deviceId)
	{
		var tuyaDevice = await _tuyaService.GetTuyaDeviceFromIdAsync(deviceId);
		
		var response = await tuyaDevice.SendAsync(TuyaCommand.CONTROL, tuyaDevice.FillJson("{\"dps\":{\"1\":false}}"));
		Console.WriteLine($"Response JSON: {response.JSON}");
	}

	public Task SetBrightnessLevelAsync(string deviceId)
	{
		throw new System.NotImplementedException();
	}

	private void SetTuyaDevice(TuyaDeviceApiInfo tuyaDeviceApiInfo)
	{
		_tuyaDeviceApiInfo ??= tuyaDeviceApiInfo;
		_tuyaDevice ??= new TuyaDevice(tuyaDeviceApiInfo.Ip, tuyaDeviceApiInfo.LocalKey, tuyaDeviceApiInfo.Id);
	}
}