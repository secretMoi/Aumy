using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aumy.Devices.NestThermostat.Commands;
using Aumy.Devices.NestThermostat.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aumy.Devices.NestThermostat;

public class NestThermostat
{
	private readonly HeatMode _heatMode;
	private readonly EcoMode _ecoMode;
	private readonly HttpClient _httpClient;
	private readonly GoogleNestConfiguration _googleNestConfiguration;
	private RefreshToken _refreshToken;
	
	public NestThermostat(IOptions<GoogleNestConfiguration> googleNestConfiguration, HeatMode heatMode, EcoMode ecoMode)
	{
		_heatMode = heatMode;
		_ecoMode = ecoMode;
		_refreshToken = new RefreshToken();
		_googleNestConfiguration = googleNestConfiguration.Value;
        
		_httpClient = new HttpClient();
		SetAuthorizationHeader();

		// set une addresse de base (ex : http://xkcd.com/ , qui permet de manipuler plusieurs liens api de ce site)
		//_httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
		_httpClient.DefaultRequestHeaders.Accept.Clear(); // nettoie les headers
		
		_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	}
	
	private StringContent SerializeAsJson<T>(T dto)
	{
		var json = JsonConvert.SerializeObject(dto);
		return new StringContent(json, Encoding.UTF8, "application/json");
	}

	private async Task CheckIfTokenIsValidAsync()
	{
		var currentTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
		if (_googleNestConfiguration.Token == null ||
		    string.IsNullOrEmpty(_googleNestConfiguration.Token) ||
		    _refreshToken.Timestamp + _refreshToken.ExpiresIn <= currentTimestamp)
		{
			await RefreshTokenAsync();
		}
	}
    
	public async Task<GoogleNestDevices> GetAllDevicesAsync(bool hasAlreadyTried = false)
	{
		await CheckIfTokenIsValidAsync();
		using var response = await _httpClient.GetAsync(_googleNestConfiguration.Url);
        
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadAsAsync<GoogleNestDevices>();
		}
		if (response.StatusCode == HttpStatusCode.Unauthorized && !hasAlreadyTried)
		{
			await RefreshTokenAsync();
			return await GetAllDevicesAsync(true);
		}

		throw new Exception(response.ReasonPhrase);
	}

	private async Task RefreshTokenAsync()
	{
		using var response = await _httpClient.PostAsync(GetRefreshTokenUrl(), null);
        
		if (response.IsSuccessStatusCode)
		{
			_refreshToken = await response.Content.ReadAsAsync<RefreshToken>();
			_refreshToken.Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
			_googleNestConfiguration.Token = _refreshToken.AccessToken;
			SetAuthorizationHeader();
		}
	}

	private string GetRefreshTokenUrl()
	{
		return _googleNestConfiguration.RefreshTokenUrl +
		       $"&client_id={_googleNestConfiguration.ClientId}" +
		       $"&client_secret={_googleNestConfiguration.ClientSecret}" +
		       $"&refresh_token={_googleNestConfiguration.RefreshToken}";
	}

	private void SetAuthorizationHeader()
	{
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
			"Bearer",
			_googleNestConfiguration.Token
		);
	}

	public async Task SetTemperatureAsync(double temperature)
	{
		var temperatureCommand = new SetTemperatureCommand
		{
			Command = "sdm.devices.commands.ThermostatTemperatureSetpoint.SetHeat",
			Parameters = new SetTemperatureCommand.Params
			{
				HeatCelsius = temperature
			}
		};

		await SendCommandAsync(temperatureCommand);
	}

	public async Task SetHeatModeAsync(string heatMode)
	{
		if (!_heatMode.IsValidHeatModeValue(heatMode))
		{
			throw new ArgumentException($"Heat mode ({heatMode}) is not valid");
		}
		
		var heatModeCommand = new SetModeCommand
		{
			Command = "sdm.devices.commands.ThermostatMode.SetMode",
			Parameters = new SetModeCommand.Params
			{
				Mode = heatMode
			}
		};

		await SendCommandAsync(heatModeCommand);
	}
	
	public async Task SetEcoModeAsync(string ecoMode)
	{
		if (!_ecoMode.IsValidEcoModeValue(ecoMode))
		{
			throw new ArgumentException($"Eco mode ({ecoMode}) is not valid");
		}
		
		var ecoModeCommand = new SetModeCommand
		{
			Command = "sdm.devices.commands.ThermostatEco.SetMode",
			Parameters = new SetModeCommand.Params
			{
				Mode = ecoMode
			}
		};

		await SendCommandAsync(ecoModeCommand);
	}

	private async Task SendCommandAsync(IGoogleNestCommand googleNestCommand, bool hasAlreadyTried = false)
	{
		await CheckIfTokenIsValidAsync();
		using var response = await _httpClient.PostAsync(
			_googleNestConfiguration.Url + $"/{_googleNestConfiguration.DeviceId}:executeCommand",
			SerializeAsJson(googleNestCommand)
		);
        
		if (response.IsSuccessStatusCode) { return; }
		if (response.StatusCode == HttpStatusCode.Unauthorized && !hasAlreadyTried)
		{
			await RefreshTokenAsync();
			await SendCommandAsync(googleNestCommand, true);
			return;
		}

		throw new Exception(response.ReasonPhrase);
	}
}