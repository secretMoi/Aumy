using System.Threading;
using System.Threading.Tasks;
using Aumy.Devices.Tuya;
using Microsoft.Extensions.Hosting;

namespace Aumy.Services;

public class ScanTuyaDevices : IHostedService
{
	private readonly TuyaService _tuyaService;

	public ScanTuyaDevices(TuyaService tuyaService)
	{
		_tuyaService = tuyaService;
	}
	
	public Task StartAsync(CancellationToken cancellationToken)
	{
		_tuyaService.ScanAsync().ConfigureAwait(false);
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}