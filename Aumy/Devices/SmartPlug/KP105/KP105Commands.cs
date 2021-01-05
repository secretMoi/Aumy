using Newtonsoft.Json;

namespace Aumy.Devices.SmartPlug.KP105
{
	public class KP105Commands
	{
		public string GetAllInformations()
		{
			return JsonConvert.SerializeObject(new
			{
				system = new
				{
					get_sysinfo = new
					{
					}
				}
			});
		}
		
		public string TurnOn()
		{
			return JsonConvert.SerializeObject(new
			{
				system = new
				{
					set_relay_state = new
					{
						state = 1
					}
				}
			});
		}
		
		public string TurnOff()
		{
			return JsonConvert.SerializeObject(new
			{
				system = new
				{
					set_relay_state = new
					{
						state = 0
					}
				}
			});
		}

		public string GetPowerState()
		{
			return JsonConvert.SerializeObject(new
			{
				system = new
				{
					get_sysinfo = new
					{
						relay_state = new
						{
						}
					}
				}
			});
		}
		
		public string SetAlias(string name)
		{
			return JsonConvert.SerializeObject(new
			{
				system = new
				{
					set_dev_alias = new
					{
						alias = name
					}
				}
			});
		}
		
		public string GetAlias(string name)
		{
			return JsonConvert.SerializeObject(new
			{
				system = new
				{
					set_dev_alias = new
					{
					}
				}
			});
		}
		
		public string GetCloudInformations()
		{
			return JsonConvert.SerializeObject(new
			{
				cnCloud = new
				{
					get_info = new
					{
					}
				}
			});
		}
	}
}