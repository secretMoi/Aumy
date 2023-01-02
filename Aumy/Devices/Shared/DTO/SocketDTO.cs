namespace Aumy.Devices.Shared.DTO;

public class SocketDTO
{
	public bool? State { get; set; }
	public float? Current { get; set; }

	public float? Power { get; set; }

	public float? Voltage { get; set; }
}