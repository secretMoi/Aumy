using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Aumy.Devices.NestThermostat;

public class NestThermostat
{
    private HttpClient _httpClient;
    private string _url = "https://smartdevicemanagement.googleapis.com/v1/enterprises/898e7adf-5985-4fbe-a2c1-33727cc40b4a/devices";
    
    public NestThermostat()
    {
        // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        // ServicePointManager.ServerCertificateValidationCallback +=
        //     (sender, cert, chain, sslPolicyErrors) => true;

        _httpClient = new HttpClient();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", "ya29.a0ARrdaM_0WYf2Eif2Z8tJuLSQVXxXehxMeGsb88an3VBZQgODz2sCj0jBSCr5gkjrUD-RcOUoBKLR6xeTJpD501DDCUJKJ2pz5kjD0jgNUKdmo97SaMkKm-j4xgm-4jJViVh5Fmtkea1IE6T-HNOJQf6oxkHW");

        // set une addresse de base (ex : http://xkcd.com/ , qui permet de manipuler plusieurs liens api de ce site)
        //_httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
        //_httpClient.BaseAddress = new Uri("http://aorus.aorus.ovh/restserver/");

        _httpClient.DefaultRequestHeaders.Accept.Clear(); // nettoie les headers

        // crée un header qui demande du json
        //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    public async Task<string> GetAllDevicesAsync()
    {
        var response = await _httpClient.GetStringAsync(_url);
        return response;
    }
}