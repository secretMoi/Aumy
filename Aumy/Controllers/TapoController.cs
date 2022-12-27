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

	public async Task<IActionResult> Login()
	{
		await _tapoConnection.LoginWithIP();

		return Ok();
	}

	[HttpGet("TurnOff")]
	public async Task<IActionResult> TurnOff()
	{
		var deviceInfo = await _tapoConnection.LoginWithIP();
		await _tapoConnection.ChangeState(false, deviceInfo);
		return Ok();
	}

	[HttpGet("TurnOn")]
	public async Task<IActionResult> TurnOn()
	{
		var deviceInfo = await _tapoConnection.LoginWithIP();
		await _tapoConnection.ChangeState(true, deviceInfo);

		return Ok();
	}

	[HttpGet("GetInfo")]
	public async Task<IActionResult> GetInfo()
	{
		var deviceInfo = await _tapoConnection.LoginWithIP();
		return Ok(await _tapoConnection.GetInfo(deviceInfo));

		
	}
}