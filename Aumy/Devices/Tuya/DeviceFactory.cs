using System;
using System.Linq;
using Aumy.Devices.Tuya.Devices;
using Aumy.Devices.Tuya.Devices.Interfaces;
using Aumy.Devices.Tuya.Devices.MoesDimmerSwitch;
using Microsoft.Extensions.Options;

namespace Aumy.Devices.Tuya;

public class DeviceFactory
{
	private readonly MoesDimmerSwitch _moesDimmerSwitch;
	private readonly TuyaConfiguration _tuyaConfiguration;

	public DeviceFactory(IOptions<TuyaConfiguration> tuyaConfiguration, MoesDimmerSwitch moesDimmerSwitch)
	{
		_moesDimmerSwitch = moesDimmerSwitch;
		_tuyaConfiguration = tuyaConfiguration.Value;
	}
	
	public IDimmerSwitch GetDimmerSwitch(string deviceId)
	{
		var localTuyaDevice = _tuyaConfiguration.Devices.FirstOrDefault(x => x.TuyaDeviceId.Equals(deviceId));
		if (localTuyaDevice is null)
			throw new ArgumentNullException();

		return localTuyaDevice.InternalName switch
		{
			DeviceName.MoesDimmerSwitch => _moesDimmerSwitch,
			_ => null
		};
	}
}