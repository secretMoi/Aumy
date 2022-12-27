using System;
using System.Security.Cryptography;
using System.Text;
using Aumy.Devices.Tapo.Models;

namespace Aumy.Devices.Tapo;

public class KeyPair
{
	private byte[] _privateKey;
	private byte[] _publicKey;
	private readonly TapoConfiguration _tapoConfiguration;

	public KeyPair(TapoConfiguration tapoConfiguration)
	{
		_tapoConfiguration = tapoConfiguration;
	}

	public KeyPair()
	{
		var rsa = RSA.Create(1024);

		/*_privateKey = rsa.ExportPkcs8PrivateKey("top secret",
		    new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 1)
		    );*/

		_privateKey = rsa.ExportPkcs8PrivateKey();

		_publicKey = rsa.ExportSubjectPublicKeyInfo();
	}

	public string GetPublicKeyPem()
	{
		/*return new string(PemEncoding.Write("PUBLIC KEY", _publicKey));*/
		return _tapoConfiguration.PublicKey;
	}

	public string Encrypt(string data)
	{
		var rsa = RSA.Create(1024);
		/*rsa.ImportSubjectPublicKeyInfo(_publicKey, out int bRead);*/
		rsa.ImportFromPem(GetPublicKeyPem());

		byte[] decryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(data), RSAEncryptionPadding.Pkcs1);

		return Convert.ToBase64String(decryptedBytes);
	}

	public byte[] Decrypt(string base64)
	{
		var rsa = RSA.Create(1024);
		/*rsa.ImportEncryptedPkcs8PrivateKey("top secret", _privateKey, out int bytesRead);*/
		/*rsa.ImportPkcs8PrivateKey(_privateKey, out int bytesRead);*/

		rsa.ImportFromEncryptedPem(_tapoConfiguration.PrivateKey, _tapoConfiguration.KeyPassword);

		byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(base64), RSAEncryptionPadding.Pkcs1);
		
		return decryptedBytes;
	}
}