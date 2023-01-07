namespace Aumy.Repositories.Models;

public class Socket
{
	public int Id { get; set; }
	public bool? State { get; set; }
	public float? Current { get; set; }

	public float? Power { get; set; }

	public float? Voltage { get; set; }
}