using System.ComponentModel.DataAnnotations;

namespace Aumy.Repositories.Models;

public class Device
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	public string Icon { get; set; }
	public bool? IsOnline { get; set; }
	
	public bool IsTuyaDevice { get; set; }
	public int? TuyaDeviceId { get; set; }
	public TuyaDevice TuyaDevice { get; set; }
	
	public int? SwitchId { get; set; }
	public Switch Switch { get; set; }
	
	public int? SocketId { get; set; }
	public Socket Socket { get; set; }
	
	[Required]
	public int DeviceTypeId { get; set; }
	[EnumDataType(typeof(DeviceType))]
	public DeviceType DeviceType
	{
		get => (DeviceType)DeviceTypeId;
		set => DeviceTypeId = (int)value;
	}
}