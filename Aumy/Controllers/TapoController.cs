using System.Threading.Tasks;
using Aumy.Devices.Tapo;
using Microsoft.AspNetCore.Mvc;

namespace Aumy.Controllers;

[ApiController]
[Route("[controller]")]
public class TapoController : ControllerBase
{
	private readonly TapoConnection _tapoConnection;

	public TapoController(TapoConnection tapoConnection)
	{
		_tapoConnection = tapoConnection;
	}

	
	[HttpGet("login")]
	public async Task<IActionResult> Login()
	{
		await _tapoConnection.LoginWithIP();

		return Ok();
	}

	[HttpGet("turn-off")]
	public async Task<IActionResult> TurnOff()
	{
		var deviceInfo = await _tapoConnection.LoginWithIP();
		await _tapoConnection.ChangeState(false, deviceInfo);
		return Ok();
	}

	[HttpGet("turn-on")]
	public async Task<IActionResult> TurnOn()
	{
		var deviceInfo = await _tapoConnection.LoginWithIP();
		await _tapoConnection.ChangeState(true, deviceInfo);

		return Ok();
	}

	[HttpGet("get-info")]
	public async Task<IActionResult> GetInfo()
	{
		var deviceInfo = await _tapoConnection.LoginWithIP();
		return Ok(await _tapoConnection.GetInfo(deviceInfo));
	}

	[HttpGet("change-color/{red}/{green}/{blue}")]
	public async Task<IActionResult> ChangeColor(string red, string green, string blue)
	{
		var deviceInfo = await _tapoConnection.LoginWithIP();
		await _tapoConnection.SetColor(red, green, blue, deviceInfo);
		return Ok();
	}
}