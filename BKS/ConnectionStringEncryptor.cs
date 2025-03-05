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
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            byte[] encryptedData = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(plainText),
                null,
                DataProtectionScope.LocalMachine
            );

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// Şifreli connection string'i çözer.
        /// </summary>
        public static string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                throw new ArgumentNullException(nameof(encryptedText));

            byte[] decryptedData = ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedText),
                null,
                DataProtectionScope.LocalMachine
            );

            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}
