using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BKS
{
    public static class ConnectionStringEncryptor
    {
        /// <summary>
        /// Connection string'i şifreler.
        /// </summary>
        public static string Encrypt(string plainText, string password)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            byte[] entropy = Encoding.UTF8.GetBytes(password ?? "");
            byte[] encryptedData = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(plainText),
                entropy,
                DataProtectionScope.LocalMachine
            );

            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string encryptedText, string password)
        {
            if (string.IsNullOrEmpty(encryptedText))
                throw new ArgumentNullException(nameof(encryptedText));

            byte[] entropy = Encoding.UTF8.GetBytes(password ?? "");
            byte[] decryptedData = ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedText),
                entropy,
                DataProtectionScope.LocalMachine
            );

            return Encoding.UTF8.GetString(decryptedData);
        }

    }
}
