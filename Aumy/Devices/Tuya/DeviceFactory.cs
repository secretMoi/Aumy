using System;
using System.Linq;
using Aumy.Devices.Tuya.Devices;
using Aumy.Devices.Tuya.Devices.Interfaces;
using Aumy.Devices.Tuya.Devices.KonyksSocket;
using Aumy.Devices.Tuya.Devices.MoesDimmerSwitch;
using Microsoft.Extensions.Options;

namespace Aumy.Devices.Tuya;

public class DeviceFactory
{
	private readonly MoesDimmerSwitch _moesDimmerSwitch;
	private readonly KonyksPriska _konyksPriska;
	private readonly TuyaConfiguration _tuyaConfiguration;

	public DeviceFactory(IOptions<TuyaConfiguration> tuyaConfiguration,
		MoesDimmerSwitch moesDimmerSwitch, KonyksPriska konyksPriska)
	{
		_moesDimmerSwitch = moesDimmerSwitch;
		_konyksPriska = konyksPriska;
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
	
	public ISocket GetSocket(string deviceId)
	{
		var localTuyaDevice = _tuyaConfiguration.Devices.FirstOrDefault(x => x.TuyaDeviceId.Equals(deviceId));
		if (localTuyaDevice is null)
			throw new ArgumentNullException();

		return localTuyaDevice.InternalName switch
		{
			DeviceName.KonyksSocket => _konyksPriska,
			_ => null
		};
	}
}