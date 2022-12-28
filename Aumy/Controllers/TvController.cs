using System.Threading.Tasks;
using Aumy.Devices.Tv;
using Microsoft.AspNetCore.Mvc;

namespace Aumy.Controllers;

[ApiController]
[Route("[controller]")]
public class TvController : ControllerBase
{
	private readonly TvService _tvService;

	public TvController(TvService tvService)
	{
		_tvService = tvService;
	}
	
	public async Task<IActionResult> Test()
	{
		var service = new WebOsTv.Net.Service();
			
		await service.ConnectAsync("192.168.0.102");
		await service.Audio.MuteAsync();

		service.Close();

		return Ok();
	}
}