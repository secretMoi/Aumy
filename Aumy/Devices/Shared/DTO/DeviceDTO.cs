namespace Aumy.Devices.Shared.DTO;

public class DeviceDTO
{
	public DeviceTypeDTO DeviceType { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	public string Icon { get; set; }
	public bool? IsOnline { get; set; }
	
	public bool IsTuyaDevice { get; set; }
	public TuyaDeviceDTO TuyaDevice { get; set; }
	
	
	public SwitchDTO Switch { get; set; }
	public SocketDTO Socket { get; set; }
}