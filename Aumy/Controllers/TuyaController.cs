using System.Threading.Tasks;
using Aumy.Devices.Tuya;
using Microsoft.AspNetCore.Mvc;

namespace Aumy.Controllers;

[ApiController]
[Route("[controller]")]
public class TuyaController : ControllerBase
{
	private readonly TuyaService _tuyaService;

	public TuyaController(TuyaService tuyaService)
	{
		_tuyaService = tuyaService;
	}
	
	[HttpGet("scan")]
	public async Task<IActionResult> Scan()
	{
		await _tuyaService.Scan();

		return Ok();
	}
	
	[HttpGet("device-list")]
	public async Task<IActionResult> DeviceList()
	{
		return Ok(await _tuyaService.DeviceList());
	}
	
	[HttpGet("device-info/{deviceId}")]
	public async Task<IActionResult> DeviceInfo(string deviceId)
	{
		return Ok(await _tuyaService.GetDeviceInfoAsync(deviceId));
	}
	
	[HttpGet("device-commands/{deviceId}")]
	public async Task<IActionResult> DeviceCommands(string deviceId)
	{
		return Ok(await _tuyaService.GetDeviceCommandsAsync(deviceId));
	}
	
	[HttpGet("communicate/{deviceId}")]
	public async Task<IActionResult> Communicate(string deviceId)
	{
		await _tuyaService.CommunicateAsync(deviceId);

		return Ok();
	}
}