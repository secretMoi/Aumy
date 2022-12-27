using System;
using System.IO;
using System.Security.Cryptography;

namespace Aumy.Devices.Tapo;

public class RequestCipher
{
	private readonly byte[] _Key, _IV;

	public RequestCipher(byte[] Key, byte[] IV)
	{
		_Key = Key;
		_IV = IV;
	}
	public string Encrypt(string json)
	{
		byte[] encrypted;

		using (var aes = new AesManaged())
		{
			var encryptor = aes.CreateEncryptor(_Key, _IV);
			using (var ms = new MemoryStream())
			{
				using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
				{
					using (var sw = new StreamWriter(cs))
					{
						sw.Write(json);
					}
					encrypted = ms.ToArray();
				}
			}
		}
		return Convert.ToBase64String(encrypted);
	}
	public string Decrypt(string base64)
	{
		using var aes = new AesManaged();
		var decryptor = aes.CreateDecryptor(_Key, _IV);
		using var ms = new MemoryStream(Convert.FromBase64String(base64));
		using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
		using var reader = new StreamReader(cs);
		return reader.ReadToEnd();
	}
}