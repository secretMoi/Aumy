using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Aumy.Devices.Tapo.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aumy.Devices.Tapo;

public class TapoConnection
{
	private readonly TapoConfiguration _tapoConfiguration;
	private readonly HttpClient _httpClient = new();
	private readonly string _email, _password, _deviceIP;

	private RequestCipher _requestCipher;
	private KeyPair _keyPair;

	public TapoConnection(IOptions<TapoConfiguration> tapoConfiguration)
	{
		_tapoConfiguration = tapoConfiguration.Value;
		_email = _tapoConfiguration.Email;
		_password = _tapoConfiguration.Password;
		_deviceIP = _tapoConfiguration.DeviceIP;
	}

	private async Task<string> SecurePasstrough(string request, string cookie, string token = "")
	{
		object passtroughRequest = new BasicRequest
		{
			Method = "securePassthrough",
			Params = new BasicRequestParams
			{
				Request = _requestCipher.Encrypt(request)
			}
		};

		var response = await RequestWithHeader(
			$"http://{_deviceIP}/app?token={token}",
			JsonConvert.SerializeObject(passtroughRequest),
			cookie
		);

		return _requestCipher.Decrypt(response);
	}

	public async Task<DeviceInfo> LoginWithIP()
	{
		var deviceInfo = await Handshake();

		_requestCipher = new RequestCipher(deviceInfo.Key, deviceInfo.IV);

		object loginDeviceRequest = new LoginDeviceRequest
		{
			Method = "login_device",
			RequestTimeMils = 0,
			Params = new LoginDeviceRequestParams
			{
				Username = Convert.ToBase64String(Encoding.UTF8.GetBytes(ShaDigest(_email))),
				Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(_password)),
			}
		};

		var responseJson = await SecurePasstrough(
			JsonConvert.SerializeObject(loginDeviceRequest),
			deviceInfo.SessionId
		);

		var response = JsonConvert.DeserializeObject<LoginDeviceResponseDecrypted>(responseJson);

		deviceInfo.Token = response.Result.Token;

		return deviceInfo;
	}
	
	public async Task<string> GetInfo(DeviceInfo deviceInfo)
	{
		var request = new GetInfoRequest
		{
			Method = "get_device_info"
		};
		
		return await SendRequest(request, deviceInfo);
	}

	public async Task ChangeState(bool state, DeviceInfo deviceInfo)
	{
		var request = new ChangeStateRequest
		{
			Method = "set_device_info",
			Params = new DeviceStateParams
			{
				DeviceOn = state
			}
		};
		
		await SendRequest(request, deviceInfo);
	}

	public async Task SetColor(string red, string green, string blue, DeviceInfo deviceInfo)
	{
		var colorRGB = ColorTranslator.FromHtml($"#{red}{green}{blue}");

		var colorHSL = ColorsToHSL.FromRGBToHSL(colorRGB.R, colorRGB.G, colorRGB.B);

		var hue = Convert.ToInt16(colorHSL.Hue);
		var saturation = Convert.ToInt16(colorHSL.Saturation * 100);
		var brightness = Convert.ToInt16(colorHSL.Lightness * 100);

		SetDeviceInfoColorRequest request;

		if (hue < 5 && saturation < 20)
		{
			request = new SetDeviceInfoColorRequest
			{
				Method = "set_device_info",
				Params = new SetDeviceInfoColorRequestParams
				{
					Brightness = brightness,
					ColorTemp = 4500
				}
			};
		}
		else
		{
			request = new SetDeviceInfoColorRequest
			{
				Method = "set_device_info",
				Params = new SetDeviceInfoColorRequestParams
				{
					Hue = hue,
					Saturation = saturation,
					Brightness = brightness,
					ColorTemp = 0
				}
			};
		}

		await SendRequest(request, deviceInfo);
	}

	private async Task<string> SendRequest(ITapoRequest request, DeviceInfo deviceInfo)
	{
		var json = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

		return await SecurePasstrough(
			json,
			deviceInfo.SessionId,
			deviceInfo.Token
		);
	}

	private string ShaDigest(string data)
	{
		return Convert.ToHexString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(data)));
	}

	private async Task<DeviceInfo> Handshake()
	{
		_keyPair = new KeyPair(_tapoConfiguration);

		object handshake = new HandshakeRequest
		{
			Method = "handshake",
			Params = new HandshakeRequestParams
			{
				Key = _tapoConfiguration.PublicKey
			},
			RequestTimeMils = 0
		};

		var httpContent = new StringContent(JsonConvert.SerializeObject(handshake), Encoding.UTF8, "application/json");
		var uri = new Uri($"http://{_deviceIP}/app");

		var response = await _httpClient.PostAsync($"http://{_deviceIP}/app", httpContent);

		var cookies = new CookieContainer();

		foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
		{
			cookies.SetCookies(uri, cookieHeader);
		}

		var cookieValue = cookies.GetCookies(uri).FirstOrDefault(c => c.Name == "TP_SESSIONID")?.Value;

		var dataResponse = JsonConvert.DeserializeObject<HandshakeResponse>(await response.Content.ReadAsStringAsync());

		var deviceKeyIvBytes = _keyPair.Decrypt(dataResponse.Result.Key);

		var KeyArray = new byte[16];
		var IVArray = new byte[16];

		Array.Copy(deviceKeyIvBytes, 0, KeyArray, 0, 16);
		Array.Copy(deviceKeyIvBytes, 16, IVArray, 0, 16);

		var deviceInfo = new DeviceInfo
		{
			Key = KeyArray,
			IV = IVArray,
			SessionId = cookieValue
		};

		return deviceInfo;
	}

	private async Task<string> RequestWithHeader(string uri, string json, string cookie)
	{
		string responseJson;

		var handler = new HttpClientHandler();

		handler.AutomaticDecompression = ~DecompressionMethods.None;

		using (var httpClient = new HttpClient(handler))
		{
			using (var request = new HttpRequestMessage(new HttpMethod("POST"), uri))
			{
				request.Headers.TryAddWithoutValidation("Cookie", $"TP_SESSIONID={cookie}");

				request.Content = new StringContent(json);

				var responseRaw = await httpClient.SendAsync(request);
				responseJson = await responseRaw.Content.ReadAsStringAsync();
			}
		}

		var response = JsonConvert.DeserializeObject<LoginDeviceResponse>(responseJson);

		return response.Result.Response;
	}
}