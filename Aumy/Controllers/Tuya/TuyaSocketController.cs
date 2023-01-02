using System.Threading.Tasks;
using Aumy.Devices.Tuya;
using Microsoft.AspNetCore.Mvc;

namespace Aumy.Controllers.Tuya;

[ApiController]
[Route("tuya/socket")]
public class TuyaSocketController : ControllerBase
{
	private readonly DeviceFactory _deviceFactory;

	public TuyaSocketController(DeviceFactory deviceFactory)
	{
		_deviceFactory = deviceFactory;
	}
	
	[HttpPost("{deviceId}/turn-on")]
	public async Task<IActionResult> TurnOn(string deviceId)
	{
		await _deviceFactory.GetSocket(deviceId).TurnOnAsync(deviceId);

		return Ok();
	}
	
	[HttpPost("{deviceId}/turn-off")]
	public async Task<IActionResult> TurnOff(string deviceId)
	{
		await _deviceFactory.GetSocket(deviceId).TurnOffAsync(deviceId);

		return Ok();
	}
}