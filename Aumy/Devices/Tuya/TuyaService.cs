using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.clusterrr.TuyaNet;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aumy.Devices.Tuya;

public class TuyaService
{
	private readonly TuyaConfiguration _tuyaConfiguration;
	private static readonly Dictionary<string, string> _devicesIp = new();

	public TuyaService(IOptions<TuyaConfiguration> tuyaConfiguration)
	{
		_tuyaConfiguration = tuyaConfiguration.Value;
	}
	
	public async Task<TuyaDeviceApiInfo[]> DeviceList()
	{
		var tuyaApi = new TuyaApi(region: TuyaApi.Region.CentralEurope, _tuyaConfiguration.AccessId, _tuyaConfiguration.ApiSecret);
		var devices = await tuyaApi.GetAllDevicesInfoAsync(_tuyaConfiguration.DefaultDeviceId);
		
		foreach(var device in devices)
			Console.WriteLine($"Device: {device.Name}, device ID: {device.Id}, local key: {device.LocalKey}");

		return devices;
	}
	
	public async Task<TuyaDeviceApiInfo> GetDeviceInfoAsync(string deviceId)
	{
		var tuyaApi = new TuyaApi(region: TuyaApi.Region.CentralEurope, _tuyaConfiguration.AccessId, _tuyaConfiguration.ApiSecret);
		var tuyaDeviceApiInfo = await tuyaApi.GetDeviceInfoAsync(deviceId);

		return tuyaDeviceApiInfo;
	}
	
	public async Task ScanAsync()
	{
		var scanner = new TuyaScanner();
		scanner.OnNewDeviceInfoReceived += Scanner_OnNewDeviceInfoReceived;
		Console.WriteLine("Scanning local network for Tuya devices");
		scanner.Start();
		await Task.Delay(10000);
		scanner.Stop();
	}
	
	public async Task<TuyaLocalResponse> GetDeviceCommandsAsync(string deviceId)
	{
		var tuyaDevice = await GetTuyaDeviceFromIdAsync(deviceId);
		
		var deviceInfoRequest = new DeviceInfoRequest
		{
			GwId = tuyaDevice.DeviceId,
			DevId = tuyaDevice.DeviceId,
			Uid = tuyaDevice.DeviceId,
			Timestamp = DateTime.Now.ToUniversalTime().Ticks.ToString()
		};
		
		byte[] request = tuyaDevice.EncodeRequest(
			TuyaCommand.DP_QUERY,
			JsonConvert.SerializeObject(deviceInfoRequest)
		);
		
		byte[] encryptedResponse = await tuyaDevice.SendAsync(request);
		var response = tuyaDevice.DecodeResponse(encryptedResponse);
		Console.WriteLine($"Response JSON: {response.JSON}");

		return response;
	}

	public async Task CommunicateAsync(string deviceId)
	{
		var tuyaDevice = await GetTuyaDeviceFromIdAsync(deviceId);
		
		var response = await tuyaDevice.SendAsync(TuyaCommand.CONTROL, tuyaDevice.FillJson("{\"dps\":{\"1\":true}}"));
		Console.WriteLine($"Response JSON: {response.JSON}");
	}

	public async Task<TuyaDevice> GetTuyaDeviceFromIdAsync(string deviceId)
	{
		var tuyaDeviceApiInfo = await GetDeviceInfoAsync(deviceId);

		if (tuyaDeviceApiInfo is null)
			throw new ArgumentNullException();

		return new TuyaDevice(GetDeviceIpById(tuyaDeviceApiInfo.Id), tuyaDeviceApiInfo.LocalKey, tuyaDeviceApiInfo.Id);
	}

	private string GetDeviceIpById(string deviceId)
	{
		return _devicesIp.FirstOrDefault(x => x.Key.Equals(deviceId)).Value;
	}
	
	private static void Scanner_OnNewDeviceInfoReceived(object sender, TuyaDeviceScanInfo e)
	{
		Console.WriteLine($"New device found! IP: {e.IP}, ID: {e.GwId}, version: {e.Version}");
		_devicesIp.Add(e.GwId, e.IP);
	}
}