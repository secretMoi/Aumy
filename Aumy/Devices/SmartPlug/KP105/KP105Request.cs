using System.Threading.Tasks;

namespace Aumy.Devices.SmartPlug.KP105
{
	public class KP105Request
	{
		private KP105 _kp105;
		
		public KP105Request(string ip)
		{
			_kp105 = new KP105(ip);
		}
		
		/*public async Task<dynamic> GetAllInformationsAsync()
		{
			//return _kp105.
			//todo finir
		}*/
	}
}