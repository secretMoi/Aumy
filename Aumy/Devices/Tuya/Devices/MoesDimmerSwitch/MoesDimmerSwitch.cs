using System;
using System.Threading.Tasks;
using Aumy.Devices.Shared;
using Aumy.Devices.Tuya.Devices.Interfaces;
using com.clusterrr.TuyaNet;
using Newtonsoft.Json;

namespace Aumy.Devices.Tuya.Devices.MoesDimmerSwitch;

public class MoesDimmerSwitch : IDimmerSwitch
{
	private readonly TuyaService _tuyaService;

	public MoesDimmerSwitch(TuyaService tuyaService)
	{
		_tuyaService = tuyaService;
	}

	public async Task<object> GetInformationAsync(string deviceId)
	{
		var response = await _tuyaService.GetDeviceCommandsAsync(deviceId);
		var moesDimmerSwitchRequest = JsonConvert.DeserializeObject<MoesDimmerSwitchRequest>(response.JSON)?.Dps;
		if (moesDimmerSwitchRequest != null)
			moesDimmerSwitchRequest.Brightness /= 10;

		return moesDimmerSwitchRequest;
	}

	public async Task<SwitchDTO> GetSwitchInformationAsync(string deviceId)
	{
		var response = await _tuyaService.GetDeviceCommandsAsync(deviceId);
		var moesDimmerSwitchRequest = JsonConvert.DeserializeObject<MoesDimmerSwitchRequest>(response.JSON)?.Dps;
		if (moesDimmerSwitchRequest != null)
			moesDimmerSwitchRequest.Brightness /= 10;

		return new SwitchDTO
		{
			State = moesDimmerSwitchRequest?.State,
			Percentage = moesDimmerSwitchRequest?.Brightness
		};
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

	public async Task SetBrightnessLevelAsync(string deviceId, int brightness)
	{
		var request = GenerateRequest(brightness: brightness * 10);
		await SendRequest(deviceId, request);
	}

	private MoesDimmerSwitchRequest GenerateRequest(bool? state = null, int? brightness = null)
	{
		var request = new MoesDimmerSwitchRequest
		{
			Dps = new Dps()
		};

		if (state.HasValue)
			request.Dps.State = state.Value;

		if (brightness.HasValue)
			request.Dps.Brightness = brightness.Value;

		return request;
	}

	private async Task SendRequest(string deviceId, MoesDimmerSwitchRequest request)
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