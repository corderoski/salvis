using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Salvis.Framework.Security
{

    internal static class Crypto
    {

        private const String Key = "k0d1g0";

        private const Int32 Salt = 666;

        public static String ToCrypt(string value)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            using (var encryptor = Aes.Create())
            {
                var pdb = new PasswordDeriveBytes(Key,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytes, 0, bytes.Length);
                        cs.Close();
                    }
                    value = Convert.ToBase64String(ms.ToArray());
                }
            }
            return value;
        }

        public static String ToDecrypt(string value)
        {
            var cipherBytes = Convert.FromBase64String(value);
            using (var encryptor = Aes.Create())
            {
                var pdb = new PasswordDeriveBytes(Key,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    value = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return value;
        }
        
    }

}
