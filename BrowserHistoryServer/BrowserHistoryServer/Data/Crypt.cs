using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BrowserHistoryServer.Data
{
    static class Crypt
    {
        #region RSA

        public static string RSA_Decrypt(string text, string privateKey)
        {
            return Encoding.UTF8.GetString(RSA_Decrypt(Convert.FromBase64String(text), privateKey));
        }

        public static byte[] RSA_Decrypt(byte[] data, string privateKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            return rsa.Decrypt(data, false);
        }

        #endregion

        #region AES

        public static string AES_Encrypt(string clearText, string encryptionKey)
        {
            //string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                if (encryptor == null) return string.Empty;
                var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string AES_Decrypt(string cipherText, string encryptionKey)
        {
            //string EncryptionKey = "MAKV2SPBNI99212";
            var cipherBytes = Convert.FromBase64String(cipherText);
            using (var encryptor = Aes.Create())
            {
                if (encryptor == null) return string.Empty;
                var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        #endregion

        #region Хэш SHA1

        public static string SHA1_Hash(string text)
        { return Encoding.UTF8.GetString(SHA1_Hash(Encoding.UTF8.GetBytes(text))); }

        public static byte[] SHA1_Hash(byte[] data)
        { return new SHA1CryptoServiceProvider().ComputeHash(data); }

        #endregion
    }
}
