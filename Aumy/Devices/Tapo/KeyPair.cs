using System;
using System.Security.Cryptography;
using System.Text;
using Aumy.Devices.Tapo.Models;

namespace Aumy.Devices.Tapo;

public class KeyPair
{
	private readonly TapoConfiguration _tapoConfiguration;

	public KeyPair(TapoConfiguration tapoConfiguration)
	{
		_tapoConfiguration = tapoConfiguration;
	}

	public string Encrypt(string data)
	{
		var rsa = RSA.Create(1024);
		rsa.ImportFromPem(_tapoConfiguration.PublicKey);

		var decryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(data), RSAEncryptionPadding.Pkcs1);

		return Convert.ToBase64String(decryptedBytes);
	}

	public byte[] Decrypt(string base64)
	{
		var rsa = RSA.Create(1024);

		rsa.ImportFromEncryptedPem(_tapoConfiguration.PrivateKey, _tapoConfiguration.KeyPassword);

		return rsa.Decrypt(Convert.FromBase64String(base64), RSAEncryptionPadding.Pkcs1);
	}
}