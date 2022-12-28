using System.Threading.Tasks;

namespace Aumy.Devices.Tuya.Devices.Interfaces;

public interface IDimmerSwitch
{
	Task TurnOnAsync(string deviceId);
	Task TurnOffAsync(string deviceId);
	Task SetBrightnessLevelAsync(string deviceId);
}