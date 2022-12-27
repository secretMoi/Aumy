using System;
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
		try
		{

			var service = new WebOsTv.Net.Service();

// Connect using IP Address - can also use a hostname.
			await service.ConnectAsync("192.168.0.102");
			await service.Audio.MuteAsync();

			service.Close();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
		
		// var _socket = new WebSocket("ws://192.168.0.102:3000");
		// _socket.ConnectAsync();
		//
		// if (!_socket.IsAlive)
		// 	throw new Exception($"Unable to conenct to television");

		return Ok();
	}
}