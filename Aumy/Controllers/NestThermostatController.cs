﻿using System;
using System.Threading.Tasks;
using Aumy.Devices.NestThermostat;
using Microsoft.AspNetCore.Mvc;

namespace Aumy.Controllers;

[ApiController]
[Route("[controller]")]
public class NestThermostatController : ControllerBase
{
	private readonly NestThermostat _nestThermostat;

	public NestThermostatController(NestThermostat nestThermostat)
	{
		_nestThermostat = nestThermostat;
	}
    
	[HttpGet("GetAllDevices")]
	public async Task<IActionResult> GetAllDevicesAsync()
	{
		try
		{
			return Ok(await _nestThermostat.GetAllDevicesAsync());
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return Problem();
		}
	}
    
	[HttpPost("SetTemperature")]
	public async Task<IActionResult> SetTemperatureAsync([FromBody] double temperature)
	{
		try
		{
			await _nestThermostat.SetTemperatureAsync(temperature);
			return Ok();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return Problem();
		}
	}
	
	[HttpPost("SetHeatMode")]
	public async Task<IActionResult> SetHeatModeAsync([FromBody] string heatMode)
	{
		try
		{
			await _nestThermostat.SetHeatModeAsync(heatMode);
			return Ok();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return Problem();
		}
	}
	
	[HttpPost("SetEcoMode")]
	public async Task<IActionResult> SetEcoModeAsync([FromBody] string ecoMode)
	{
		try
		{
			await _nestThermostat.SetEcoModeAsync(ecoMode);
			return Ok();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return Problem();
		}
	}
}