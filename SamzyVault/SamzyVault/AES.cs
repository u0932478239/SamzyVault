using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SamzyVault
{
	class AES
	{
		private static Random random = new Random();
		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
		static string bruh = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		private static string Key1 = "wqsWqTOB3D4KzxdB75X4LJexansLtWBU";
		public static string Encryptshhh(string plainText)
		{
			try
			{
				string key = Key1;
				RijndaelManaged aes = new RijndaelManaged();
				aes.KeySize = 256;
				aes.BlockSize = 128;
				aes.Padding = PaddingMode.PKCS7;
				aes.Mode = CipherMode.CBC;

				aes.Key = encoding.GetBytes(key);
				aes.GenerateIV();

				ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
				byte[] buffer = encoding.GetBytes(plainText);

				string encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));

				String mac = "";

				mac = BitConverter.ToString(HmacSHA256(Convert.ToBase64String(aes.IV) + encryptedText, key)).Replace("-", "").ToLower();

				var keyValues = new Dictionary<string, object>
				{
					{ "iv", Convert.ToBase64String(aes.IV) },
					{ "value", encryptedText },
					{ "mac", mac },
				};

				JavaScriptSerializer serializer = new JavaScriptSerializer();

				return Convert.ToBase64String(encoding.GetBytes(serializer.Serialize(keyValues)));
			}
			catch (Exception e)
			{
				throw new Exception("Error encrypting: " + e.Message);
			}
		}
		public static string shhhh(string plainText)
		{
			try
			{
				string key = Key1;
				RijndaelManaged aes = new RijndaelManaged();
				aes.KeySize = 256;
				aes.BlockSize = 128;
				aes.Padding = PaddingMode.PKCS7;
				aes.Mode = CipherMode.CBC;
				aes.Key = encoding.GetBytes(key);

				// Base 64 decode
				byte[] base64Decoded = Convert.FromBase64String(plainText);
				string base64DecodedStr = encoding.GetString(base64Decoded);

				// JSON Decode base64Str
				JavaScriptSerializer ser = new JavaScriptSerializer();
				var payload = ser.Deserialize<Dictionary<string, string>>(base64DecodedStr);

				aes.IV = Convert.FromBase64String(payload["iv"]);

				ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, aes.IV);
				byte[] buffer = Convert.FromBase64String(payload["value"]);

				return encoding.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
			}
			catch (Exception e)
			{
				throw new Exception("Error decrypting: " + e.Message);
			}
		}
		private static readonly Encoding encoding = Encoding.UTF8;

		public static string Encrypt(string plainText)
		{
			try
			{
				string Key2 = shhhh(File.ReadAllText($@"{bruh}/b4ecfe41c8a7d0bdfcdfa530db818117"));
				string key = Key2;
				RijndaelManaged aes = new RijndaelManaged();
				aes.KeySize = 256;
				aes.BlockSize = 128;
				aes.Padding = PaddingMode.PKCS7;
				aes.Mode = CipherMode.CBC;

				aes.Key = encoding.GetBytes(key);
				aes.GenerateIV();

				ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
				byte[] buffer = encoding.GetBytes(plainText);

				string encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));

				String mac = "";

				mac = BitConverter.ToString(HmacSHA256(Convert.ToBase64String(aes.IV) + encryptedText, key)).Replace("-", "").ToLower();

				var keyValues = new Dictionary<string, object>
				{
					{ "iv", Convert.ToBase64String(aes.IV) },
					{ "value", encryptedText },
					{ "mac", mac },
				};

				JavaScriptSerializer serializer = new JavaScriptSerializer();

				return Convert.ToBase64String(encoding.GetBytes(serializer.Serialize(keyValues)));
			}
			catch (Exception e)
			{
				throw new Exception("Error encrypting: " + e.Message);
			}
		}

		public static string Decrypt(string plainText)
		{
			try
			{
				string Key2 = shhhh(File.ReadAllText($@"{bruh}/b4ecfe41c8a7d0bdfcdfa530db818117"));
				string key = Key2;
				RijndaelManaged aes = new RijndaelManaged();
				aes.KeySize = 256;
				aes.BlockSize = 128;
				aes.Padding = PaddingMode.PKCS7;
				aes.Mode = CipherMode.CBC;
				aes.Key = encoding.GetBytes(key);

				// Base 64 decode
				byte[] base64Decoded = Convert.FromBase64String(plainText);
				string base64DecodedStr = encoding.GetString(base64Decoded);

				// JSON Decode base64Str
				JavaScriptSerializer ser = new JavaScriptSerializer();
				var payload = ser.Deserialize<Dictionary<string, string>>(base64DecodedStr);

				aes.IV = Convert.FromBase64String(payload["iv"]);

				ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, aes.IV);
				byte[] buffer = Convert.FromBase64String(payload["value"]);

				return encoding.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
			}
			catch (Exception e)
			{
				throw new Exception("Error decrypting: " + e.Message);
			}
		}

		static byte[] HmacSHA256(String data, String key)
		{
			using (HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(key)))
			{
				return hmac.ComputeHash(encoding.GetBytes(data));
			}
		}
	}
}
