using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class Crypto
    {
        public static void GenerateKeys(out string publicKey, out string privateKey)
        {
            var rsa = new RSACryptoServiceProvider();

            publicKey = rsa.ToXmlString(false);
            privateKey = rsa.ToXmlString(true);
        }

        public static string TextToRsa(string plainText, string publicKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            var dataBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = rsa.Encrypt(dataBytes, false);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string RsaToText(string encryptedText, string privateKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var decryptedBytes = rsa.Decrypt(encryptedBytes, false);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public static void FileToRsa(string path, string publicKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            var dataBytes = File.ReadAllBytes(path);
            var encryptedBytes = rsa.Encrypt(dataBytes,true);

            var splitPath = path.Split('.');

            var newPath = splitPath[0] + "_Encr." + splitPath[1];

            File.WriteAllBytes(newPath, encryptedBytes);
        }

        public static void RsaToFile(string path, string privateKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            var dataBytes = File.ReadAllBytes(path);
            var decryptedBytes = rsa.Decrypt(dataBytes, true);

            var newPath = path.Replace("_Encr", "_Decr");

            File.WriteAllBytes(newPath, decryptedBytes);
        }
    }
}
