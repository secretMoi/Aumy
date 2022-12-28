using System.Threading.Tasks;

namespace Aumy.Devices.Tuya.Devices.Interfaces;

public interface IDimmerSwitch
{
	Task<object> GetInformationAsync(string deviceId);
	Task TurnOnAsync(string deviceId);
	Task TurnOffAsync(string deviceId);
	Task SetBrightnessLevelAsync(string deviceId, int brightness);
}