﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class AesEncryptionHelper
{
	private static readonly byte[] Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("YourSecurePassword"));
	private static readonly byte[] IV = Encoding.UTF8.GetBytes("Your16CharIV1234"); // 16 byte


public static string Encrypt(string plainText)
	{
		if (plainText == null)
			throw new ArgumentNullException(nameof(plainText));

		using (Aes aes = Aes.Create())
		{
			aes.Key = Key;
			aes.IV = IV;

			ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
				{
					using (StreamWriter sw = new StreamWriter(cs))
					{
						sw.Write(plainText);
					}
					return Convert.ToBase64String(ms.ToArray());
				}
			}
		}
	}

	public static string Decrypt(string cipherText)
	{
		if (cipherText == null)
			throw new ArgumentNullException(nameof(cipherText));

		using (Aes aes = Aes.Create())
		{
			aes.Key = Key;
			aes.IV = IV;

			ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

			using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
			{
				using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
				{
					using (StreamReader sr = new StreamReader(cs))
					{
						return sr.ReadToEnd();
					}
				}
			}
		}
	}
}
