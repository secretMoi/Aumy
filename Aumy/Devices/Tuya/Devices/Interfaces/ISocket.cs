using System.Threading.Tasks;
using Aumy.Devices.Shared.DTO;

namespace Aumy.Devices.Tuya.Devices.Interfaces;

public interface ISocket
{
	Task TurnOnAsync(string deviceId);
	Task TurnOffAsync(string deviceId);

	Task<SocketDTO> GetSocketAsync(string deviceId);
}