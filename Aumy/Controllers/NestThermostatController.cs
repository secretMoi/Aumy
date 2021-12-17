using System;
using System.Threading.Tasks;
using Aumy.Devices.NestThermostat;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aumy.Controllers;

[ApiController]
[Route("[controller]")]
public class NestThermostatController : ControllerBase
{
    private readonly NestThermostat _nestThermostat;

    public NestThermostatController()
    {
        _nestThermostat = new NestThermostat();
    }
    
    [HttpGet("GetAllDevices")]
    public async Task<IActionResult> GetAllDevicesAsync()
    {
        try
        {
            var devices = await _nestThermostat.GetAllDevicesAsync();
            
            return Ok(devices);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem();
        }
    }
}