using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aumy.Devices.SmartPlug.KP105;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aumy.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public async Task Get()
		{
			/*var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
				{
					Date = DateTime.Now.AddDays(index),
					TemperatureC = rng.Next(-20, 55),
					Summary = Summaries[rng.Next(Summaries.Length)]
				})
				.ToArray();*/

			KP105 kp105 = new KP105("192.168.1.126");
			await kp105.ConnectAsync();
			//await kp105.SendAsync(kp105.TurnOn());
			await kp105.SendAsync(kp105.TurnOff());
		}
	}
}