using com.clusterrr.TuyaNet;
using Microsoft.Extensions.Options;

namespace Aumy.Devices.Tuya.Devices;

public class LivingRoomSwitch
{
	private readonly TuyaConfiguration _tuyaConfiguration;
	private const string _localDeviceUuid = "bd2fed7e-9ad8-41a3-81da-e5650196504b";
	private TuyaDeviceApiInfo _tuyaDeviceApiInfo;
	private TuyaDevice _tuyaDevice;

	public LivingRoomSwitch(IOptions<TuyaConfiguration> tuyaConfiguration)
	{
		_tuyaConfiguration = tuyaConfiguration.Value;
	}

	private void SetTuyaDevice(TuyaDeviceApiInfo tuyaDeviceApiInfo)
	{
		_tuyaDeviceApiInfo ??= tuyaDeviceApiInfo;
		_tuyaDevice ??= new TuyaDevice(tuyaDeviceApiInfo.Ip, tuyaDeviceApiInfo.LocalKey, tuyaDeviceApiInfo.Id);
	}
}