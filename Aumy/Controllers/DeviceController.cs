using System.Threading.Tasks;
using Aumy.Devices;
using Microsoft.AspNetCore.Mvc;

namespace Aumy.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
	private readonly DeviceService _deviceService;

	public DeviceController(DeviceService deviceService)
	{
		_deviceService = deviceService;
	}
	
	[HttpGet("{deviceId}")]
	public async Task<IActionResult> GetById(string deviceId)
	{
		return Ok(await _deviceService.GetById(deviceId));
	}
	
	[HttpGet("list")]
	public async Task<IActionResult> DeviceList()
	{
		return Ok(await _deviceService.DeviceList());
	}
}