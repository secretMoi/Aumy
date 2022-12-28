using System.Collections.Generic;

namespace Aumy.Devices.Tuya;

public class TuyaConfiguration
{
	public string AccessId { get; set; }
	public string ApiSecret { get; set; }
	public string DefaultDeviceId { get; set; }
	public List<LocalTuyaDevice> Devices { get; set; }
}