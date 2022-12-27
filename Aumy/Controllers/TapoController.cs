using System.Threading.Tasks;
using Aumy.Devices.Tapo;
using Microsoft.AspNetCore.Mvc;

namespace Aumy.Controllers;

[ApiController]
[Route("[controller]")]
public class TapoController : ControllerBase
{
	private readonly TapoService _tapoService;

	public TapoController(TapoService tapoService)
	{
		_tapoService = tapoService;
	}

	
	[HttpGet("login")]
	public async Task<IActionResult> Login()
	{
		await _tapoService.LoginWithIP();

		return Ok();
	}

	[HttpGet("turn-off")]
	public async Task<IActionResult> TurnOff()
	{
		var deviceInfo = await _tapoService.LoginWithIP();
		await _tapoService.ChangeState(false, deviceInfo);
		return Ok();
	}

	[HttpGet("turn-on")]
	public async Task<IActionResult> TurnOn()
	{
		var deviceInfo = await _tapoService.LoginWithIP();
		await _tapoService.ChangeState(true, deviceInfo);

		return Ok();
	}

	[HttpGet("get-info")]
	public async Task<IActionResult> GetInfo()
	{
		var deviceInfo = await _tapoService.LoginWithIP();
		return Ok(await _tapoService.GetInfo(deviceInfo));
	}

	[HttpGet("change-color/{red}/{green}/{blue}")]
	public async Task<IActionResult> ChangeColor(string red, string green, string blue)
	{
		var deviceInfo = await _tapoService.LoginWithIP();
		await _tapoService.SetColor(red, green, blue, deviceInfo);
		return Ok();
	}
}