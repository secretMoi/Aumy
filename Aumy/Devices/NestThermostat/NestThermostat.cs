using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aumy.Devices.NestThermostat.Models;

namespace Aumy.Devices.NestThermostat;

public class NestThermostat
{
    private readonly HttpClient _httpClient;
    private GoogleNestConfiguration _googleNestConfiguration;
    //private const string Url = "https://smartdevicemanagement.googleapis.com/v1/enterprises/898e7adf-5985-4fbe-a2c1-33727cc40b4a/devices";

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
}