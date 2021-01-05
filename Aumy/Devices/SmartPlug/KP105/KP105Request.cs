using System.Threading.Tasks;

namespace Aumy.Devices.SmartPlug.KP105
{
	public class KP105Request
	{
		private KP105 _kp105;
		private dynamic _cacheInformation;
		
		public KP105Request(string ip)
		{
			_kp105 = new KP105(ip);
		}
		
		public async Task ConnectAsync()
		{
			await _kp105.ConnectAsync();
		}
		
		public async Task<dynamic> GetAllInformationsAsync()
		{
			if(_cacheInformation == null)
				_cacheInformation = await _kp105.SendAsync(_kp105.Kp105Commands.GetAllInformations());
			
			return _cacheInformation;
		}
		
		public async Task<dynamic> TurnOnAsync()
		{
			return await _kp105.SendAsync(_kp105.Kp105Commands.TurnOn());
		}
		
		public async Task<dynamic> TurnOffAsync()
		{
			return await _kp105.SendAsync(_kp105.Kp105Commands.TurnOff());
		}
		
		public async Task<int> GetPowerSignalAsync()
		{
			await GetAllInformationsAsync();

			return _cacheInformation?.system?.get_sysinfo?.rssi;
		}
	}
}