using System;
using System.Threading.Tasks;
using Aumy.Devices.SmartPlug.KP105;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aumy.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class KP105Controller : ControllerBase
	{
		private KP105Request _kp105Request;

		public KP105Controller()
		{
			_kp105Request = new KP105Request("192.168.1.126");
		}

		private async Task Connection()
		{
			await _kp105Request.ConnectAsync();
		}
		
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				await Connection();
				return Ok();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return NotFound();
			} 
		}
		
		[HttpGet("GetAllInformations")]
		public async Task<IActionResult> GetAllInformations()
		{
			try
			{
				await Connection();
				
				dynamic response = await _kp105Request.GetAllInformationsAsync();
				return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return Problem();
			}
		}
		
		[HttpGet("TurnOn")]
		public async Task<IActionResult> TurnOn()
		{
			try
			{
				await Connection();
				
				dynamic response = await _kp105Request.TurnOnAsync();
				return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return Problem();
			}
		}
		
		[HttpGet("TurnOff")]
		public async Task<IActionResult> TurnOff()
		{
			try
			{
				await Connection();
				
				dynamic response = await _kp105Request.TurnOffAsync();
				return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return Problem();
			}
		}
		
		[HttpGet("GetPowerState")]
		public async Task<IActionResult> GetPowerState()
		{
			try
			{
				await Connection();
				
				dynamic response = await _kp105Request.GetPowerStateAsync();
				return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return Problem();
			}
		}
	}
}