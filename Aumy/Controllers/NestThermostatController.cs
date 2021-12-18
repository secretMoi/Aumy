using System;
using System.Threading.Tasks;
using Aumy.Devices.NestThermostat;
using Aumy.Devices.NestThermostat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Aumy.Controllers;

[ApiController]
[Route("[controller]")]
public class NestThermostatController : ControllerBase
{
    private readonly NestThermostat _nestThermostat;
    private readonly GoogleNestConfiguration _googleNestConfiguration;

    public NestThermostatController(IOptions<GoogleNestConfiguration> options)
    {
        _googleNestConfiguration = options.Value;
        _nestThermostat = new NestThermostat(_googleNestConfiguration);
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