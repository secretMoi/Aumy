using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Aumy.Devices.SmartPlug.KP105
{
	public class KP105
	{
		private readonly IPAddress _ipAddress;
		private readonly int _port = 9999;
		private readonly IPEndPoint _ipEndPoint;
		private readonly Socket _socket;
		private readonly KP105Security _kp105Security;

		public KP105(string ip)
		{
			_ipAddress = IPAddress.Parse(ip);
			_ipEndPoint = new IPEndPoint(_ipAddress, _port);
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_kp105Security = new KP105Security();
		}

		public async Task ConnectAsync()
		{
			try
			{
				await _socket.ConnectAsync(_ipEndPoint);
			}
			catch (Exception e)
			{
				var message = $"Impossible de se connecter à {_ipAddress}:{_port}\n {e.Message}";
				Console.WriteLine(message);
				throw new Exception(message);
			}
		}

		public async Task<dynamic> SendAsync(string jsonPayload)
		{
			try
			{
				return await Task.Run(async () =>
				{
					_socket.Send(_kp105Security.Encrypt(jsonPayload));

					return await ReceiveAsync();
				});

			}
			catch (Exception e)
			{
				var message = $"Impossible d'envoyer le message {jsonPayload} à {_ipAddress}:{_port}\n {e.Message}";
				Console.WriteLine(message);
				throw new Exception(message);
			}
		}

		public async Task<dynamic> ReceiveAsync()
		{
			try
			{
				byte[] buffer = new byte[2048];
				_socket.ReceiveTimeout = 5000;
			
				return await Task.Run(() =>
				{
					int bytesLen = _socket.Receive(buffer);

					if (bytesLen > 0)
						return JsonConvert.DeserializeObject<dynamic>(
							_kp105Security.Decrypt(
								buffer.Take(bytesLen).ToArray()
							)
						);
					else
						throw new Exception($"Aucune réponse reçue de la prise KP105 à {_ipAddress}:{_port}");
				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw new Exception(e.Message);
			}
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
	}
}