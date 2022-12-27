using System.Threading.Tasks;
using WebOsTv.Net;

namespace Aumy.Devices.Tv;

public class TvService
{
	private readonly Service _webOsService = new ();
	private readonly string _tvIpAddress = "192.168.0.102";

	public async Task MuteAsync()
	{
		await _webOsService.ConnectAsync(_tvIpAddress);
		await _webOsService.Audio.MuteAsync();
		_webOsService.Close();
	}

	public async Task UnMuteAsync()
	{
		await _webOsService.ConnectAsync(_tvIpAddress);
		await _webOsService.Audio.UnmuteAsync();
		_webOsService.Close();
	}
}