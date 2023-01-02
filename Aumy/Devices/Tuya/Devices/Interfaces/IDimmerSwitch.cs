using System.Threading.Tasks;
using Aumy.Devices.Shared;
using Aumy.Devices.Shared.DTO;

namespace Aumy.Devices.Tuya.Devices.Interfaces;

public interface IDimmerSwitch
{
	Task<object> GetInformationAsync(string deviceId);
	Task<SwitchDTO> GetSwitchInformationAsync(string deviceId);
	Task TurnOnAsync(string deviceId);
	Task TurnOffAsync(string deviceId);
	Task SetBrightnessLevelAsync(string deviceId, int brightness);
}