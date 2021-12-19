using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aumy.Devices.NestThermostat.Commands;
using Aumy.Devices.NestThermostat.Models;
using Newtonsoft.Json;

namespace Aumy.Devices.NestThermostat;

public class NestThermostat
{
	private readonly HttpClient _httpClient;
	private readonly GoogleNestConfiguration _googleNestConfiguration;

	public NestThermostat(GoogleNestConfiguration googleNestConfiguration)
	{
		_googleNestConfiguration = googleNestConfiguration;
        
		_httpClient = new HttpClient();
		SetAuthorizationHeader();

		// set une addresse de base (ex : http://xkcd.com/ , qui permet de manipuler plusieurs liens api de ce site)
		//_httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
		_httpClient.DefaultRequestHeaders.Accept.Clear(); // nettoie les headers

		// crée un header qui demande du json
		//_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	}
	
	protected StringContent SerializeAsJson<T>(T dto)
	{
		var json = JsonConvert.SerializeObject(dto);
		return new StringContent(json, Encoding.UTF8, "application/json");
	}
    
	public async Task<GoogleNestDevices> GetAllDevicesAsync(bool hasAlreadyTried = false)
	{
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
			var refreshToken = await response.Content.ReadAsAsync<RefreshToken>();
			_googleNestConfiguration.Token = refreshToken.AccessToken;
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

	public async Task SetTemperatureAsync(double temperature, bool hasAlreadyTried = false)
	{
		var temperatureCommand = new SetTemperatureCommand()
		{
			Command = "sdm.devices.commands.ThermostatTemperatureSetpoint.SetHeat",
			Parameters = new SetTemperatureCommand.Params()
			{
				HeatCelsius = temperature
			}
		};

		using var response = await _httpClient.PostAsync(
			_googleNestConfiguration.Url + $"/{_googleNestConfiguration.DeviceId}:executeCommand",
			SerializeAsJson(temperatureCommand)
		);
        
		if (response.IsSuccessStatusCode)
		{
			return;
		}
		if (response.StatusCode == HttpStatusCode.Unauthorized && !hasAlreadyTried)
		{
			await RefreshTokenAsync();
			await SetTemperatureAsync(temperature);
			return;
		}

		throw new Exception(response.ReasonPhrase);
	}
}