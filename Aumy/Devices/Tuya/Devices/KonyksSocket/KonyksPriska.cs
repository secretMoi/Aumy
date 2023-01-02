using System;
using System.Threading.Tasks;
using Aumy.Devices.Shared.DTO;
using Aumy.Devices.Tuya.Devices.Interfaces;
using Aumy.Devices.Tuya.Devices.MoesDimmerSwitch;
using com.clusterrr.TuyaNet;
using Newtonsoft.Json;

namespace Aumy.Devices.Tuya.Devices.KonyksSocket;

public class KonyksPriska : ISocket
{
	private readonly TuyaService _tuyaService;

	public KonyksPriska(TuyaService tuyaService)
	{
		_tuyaService = tuyaService;
	}
	
	public async Task TurnOnAsync(string deviceId)
	{
		var request = GenerateRequest(true);
		await SendRequest(deviceId, request);
	}

	public async Task TurnOffAsync(string deviceId)
	{
		var request = GenerateRequest(false);
		await SendRequest(deviceId, request);
	}

	public async Task<SocketDTO> GetSocketAsync(string deviceId)
	{
		var response = await _tuyaService.GetDeviceCommandsAsync(deviceId);
		var request = JsonConvert.DeserializeObject<KonyksPriskaRequest>(response.JSON)?.Dps;

		return new SocketDTO
		{
			State = request?.State,
			Current = request?.Current,
			Voltage = request?.Voltage,
			Power = request?.Power
		};
	}
	
	private KonyksPriskaRequest GenerateRequest(bool? state = null)
	{
		var request = new KonyksPriskaRequest
		{
			Dps = new Dps()
		};

		if (state.HasValue)
			request.Dps.State = state.Value;
		return request;
	}

	private async Task SendRequest(string deviceId, KonyksPriskaRequest request)
	{
		var tuyaDevice = await _tuyaService.GetTuyaDeviceFromIdAsync(deviceId);
		
		var jsonRequest = JsonConvert.SerializeObject(
			request,
			new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
		);
		
		var response = await tuyaDevice.SendAsync(TuyaCommand.CONTROL, tuyaDevice.FillJson(jsonRequest));
		
		Console.WriteLine($"Response JSON: {response.JSON}");
	}
}