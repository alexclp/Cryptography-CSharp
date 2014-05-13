using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CryptographyProject
{
    public class Cryptographer
    {
        public static string EncryptString(string toEncrypt)
        {
            byte[] results;
            string passphrase = "secret";

            UTF8Encoding UTF8 = new UTF8Encoding();

            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = hashProvider.ComputeHash(UTF8.GetBytes(passphrase));

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] dataToEncrypt = UTF8.GetBytes(toEncrypt);

            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                results = Encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            return Convert.ToBase64String(results);
        }

        public static string DecryptString(string toDecrypt)
        {
            byte[] results;
            string passphrase = "secret";

            UTF8Encoding UTF8 = new UTF8Encoding();

            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = hashProvider.ComputeHash(UTF8.GetBytes(passphrase));

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] dataToDecrypt = Convert.FromBase64String(toDecrypt);

            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                results = Decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                hashProvider.Clear();
            }

            return UTF8.GetString(results);
        }
    }
}
