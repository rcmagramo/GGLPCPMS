using System;
using System.Text;
using System.Security.Cryptography;

namespace eQCCSoftware.Modules
{
    class Crypto
    {
        // ---------------------------------------------------
        // HANDLES ENCRYPION AND DECRYPTION OF PASSWORDS 
        // ----------------------------------------------------

        public static TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        public static MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

        public static byte[] MD5Hash(string value)
        {
            return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
        }

        public static string Encrypt(string stringToEncrypt, string key)
        {
            DES.Key = Crypto.MD5Hash(key);
            DES.Mode = CipherMode.ECB;
            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt);
            return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static string Decrypt(string encryptedString, string key)
        {
            DES.Key = Crypto.MD5Hash(key);
            DES.Mode = CipherMode.ECB;
            byte[] Buffer = Convert.FromBase64String(encryptedString);
            return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
    }
}
