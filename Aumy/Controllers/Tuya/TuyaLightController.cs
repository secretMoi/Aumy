using System.Threading.Tasks;
using Aumy.Devices.Tuya;
using Microsoft.AspNetCore.Mvc;

namespace Aumy.Controllers.Tuya;

[ApiController]
[Route("tuya/light")]
public class TuyaLightController : ControllerBase
{
	private readonly DeviceFactory _deviceFactory;

	public TuyaLightController(DeviceFactory deviceFactory)
	{
		_deviceFactory = deviceFactory;
	}

	[HttpGet("{deviceId}/info")]
	public async Task<IActionResult> GetInformation(string deviceId)
	{
		return Ok(await _deviceFactory.GetDimmerSwitch(deviceId).GetInformationAsync(deviceId));
	}

	[HttpPost("{deviceId}/turn-on")]
	public async Task<IActionResult> TurnOn(string deviceId)
	{
		await _deviceFactory.GetDimmerSwitch(deviceId).TurnOnAsync(deviceId);

		return Ok();
	}

	[HttpPost("{deviceId}/turn-off")]
	public async Task<IActionResult> TurnOff(string deviceId)
	{
		await _deviceFactory.GetDimmerSwitch(deviceId).TurnOffAsync(deviceId);

		return Ok();
	}

	[HttpPost("{deviceId}/brightness/{brightness}")]
	public async Task<IActionResult> TurnOff(string deviceId, int brightness)
	{
		await _deviceFactory.GetDimmerSwitch(deviceId).SetBrightnessLevelAsync(deviceId, brightness);

		return Ok();
	}
}