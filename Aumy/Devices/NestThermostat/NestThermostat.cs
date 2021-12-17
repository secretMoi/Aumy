using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Aumy.Devices.NestThermostat;

public class NestThermostat
{
    private readonly HttpClient _httpClient;
    private const string Url = "https://smartdevicemanagement.googleapis.com/v1/enterprises/898e7adf-5985-4fbe-a2c1-33727cc40b4a/devices";

    public NestThermostat()
    {
        _httpClient = new HttpClient();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", 
            "ya29.a0ARrdaM_eYk9ALCAjb74kbnQILO-M16PJtAtfS9ASgsrRW0MmyF2IUJ3UhpychQrjJgJ8LvoKv3wt65gY7Rej0PAShUKJxRmK-p1SMNFHDms5vj3qrq51dXHU5Wzz0XfADc8373FZ-cQTOCQGlTZ0FF8Euf5r");

        // set une addresse de base (ex : http://xkcd.com/ , qui permet de manipuler plusieurs liens api de ce site)
        //_httpClient.BaseAddress = new Uri("http://localhost:5000/api/");

        _httpClient.DefaultRequestHeaders.Accept.Clear(); // nettoie les headers

        // crée un header qui demande du json
        //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    public async Task<GoogleNestDevices> GetAllDevicesAsync()
    {
        using var response = await _httpClient.GetAsync(Url);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<GoogleNestDevices>();
        }

        throw new Exception(response.ReasonPhrase);
    }
}